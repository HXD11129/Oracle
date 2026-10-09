using Oracle.Services;

namespace Oracle.UI;

internal sealed class CueOverlayWindow : Window
{
    private readonly TimelineEngine _engine;

    private static float TimelineUiScale =>
        float.IsFinite(C.TimelineScale)
            ? Math.Clamp(C.TimelineScale, 0.5f, 2f)
            : 1f;

    private static float CueRowWidth => 280f * TimelineUiScale;
    private static float CueRowHeight => 30f * TimelineUiScale;
    private static float CueRowGap => 3f * TimelineUiScale;

    private static void DrawScaledTimelineText(
        Vector2 position,
        uint color,
        string text)
    {
        ImGui.GetWindowDrawList().AddText(
            ImGui.GetFont(),
            ImGui.GetFontSize() * TimelineUiScale,
            position,
            color,
            text);
    }

    private const ImGuiWindowFlags OverlayFlags =
        ImGuiWindowFlags.NoDecoration
        | ImGuiWindowFlags.NoSavedSettings
        | ImGuiWindowFlags.NoFocusOnAppearing
        | ImGuiWindowFlags.NoNav
        | ImGuiWindowFlags.NoDocking
        | ImGuiWindowFlags.AlwaysAutoResize
        | ImGuiWindowFlags.NoBackground;

    public CueOverlayWindow(TimelineEngine engine)
        : base("Oracle Overlay##oracleOverlay", OverlayFlags, forceMainWindow: true)
    {
        _engine = engine;
        IsOpen = true;
        RespectCloseHotkey = false;
        DisableWindowSounds = true;
    }

    public override void PreDraw()
    {
        WindowName = I18n.Get("window.overlay.title") + "##oracleOverlay";
        OverlayClickThroughUi.ApplyMousePassThrough(this, C.OverlayClickThrough);
        ImGui.SetNextWindowPos(new Vector2(C.OverlayPosX, C.OverlayPosY), ImGuiCond.Always);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, Vector2.Zero);
    }

    public override void PostDraw()
    {
        ImGui.PopStyleVar();
    }

    public override bool DrawConditions() => C.ShowOverlay && _engine.IsContextMatched;

    public override void Draw()
    {
        var upcoming = GetUpcomingForOverlay();
        var contentStart = ImGui.GetCursorScreenPos();
        var headerSize = DrawOverlayHeader(contentStart, out var rowsTop);
        var rowsBottom = upcoming.Count > 0
            ? DrawCueRows(upcoming, new Vector2(contentStart.X, rowsTop))
            : rowsTop;
        DrawOverlayDragHandle(contentStart, headerSize, rowsBottom);
    }

    private IReadOnlyList<UpcomingCue> GetUpcomingForOverlay()
    {
        var upcoming = _engine.GetUpcoming(C.LookaheadSeconds)
            .Where(u => OverlayView.IsAction(u.Cue))
            .ToList();
        var maxRows = Math.Clamp(C.OverlayMaxRows, 1, 50);
        if (upcoming.Count <= maxRows)
            return upcoming;
        return upcoming.Take(maxRows).ToList();
    }

    private Vector2 DrawOverlayHeader(Vector2 contentStart, out float rowsTop)
    {
        var headerText = OverlayView.HeaderText(_engine);
        var headerSize = ImGui.CalcTextSize(headerText) * TimelineUiScale;
        DrawScaledTimelineText(contentStart, ImGui.GetColorU32(ImGuiCol.Text), headerText);
        rowsTop = contentStart.Y + headerSize.Y + ImGui.GetStyle().ItemSpacing.Y * TimelineUiScale;
        return headerSize;
    }

    private static void DrawOverlayDragHandle(Vector2 contentStart, Vector2 headerSize, float rowsBottom)
    {
        var hitWidth = Math.Max(CueRowWidth, headerSize.X);
        var hitHeight = Math.Max(headerSize.Y, rowsBottom - contentStart.Y);
        ImGui.SetCursorScreenPos(contentStart);
        ImGui.InvisibleButton("##oracleOverlayDrag", new Vector2(hitWidth, hitHeight));
        OverlayClickThroughUi.Handle(
            () => new Vector2(C.OverlayPosX, C.OverlayPosY),
            pos =>
            {
                C.OverlayPosX = pos.X;
                C.OverlayPosY = pos.Y;
            },
            C.OverlayClickThrough);
    }

    /// <returns>Bottom Y of the last cue row.</returns>
    private static float DrawCueRows(IReadOnlyList<UpcomingCue> upcoming, Vector2 origin)
    {
        var scale = TimelineUiScale;
        var drawList = ImGui.GetWindowDrawList();
        var y = origin.Y;
        var blinkPhaseOn = OverlayView.BlinkPhaseOn;

        foreach (var item in upcoming)
        {
            var label = ActionLookup.GetName(item.Cue.ActionId);
            var showLine = OverlayView.TryHighlightLine(
                item,
                blinkPhaseOn,
                out var lineColorVec,
                out var lineThickness);
                lineThickness *= scale;
            var color = item.IsHighlighting
                ? lineColorVec
                : new Vector4(0.92f, 0.92f, 0.92f, 1f);
            var lineColor = ImGui.ColorConvertFloat4ToU32(lineColorVec);

            var rowMin = new Vector2(origin.X, y);
            var rowMax = new Vector2(origin.X + CueRowWidth, y + CueRowHeight);

            drawList.AddRectFilled(rowMin, rowMax, ImGui.ColorConvertFloat4ToU32(new Vector4(0f, 0f, 0f, 0.5f)), 4f * scale);

            if (showLine)
            {
                drawList.AddRect(
                    rowMin,
                    rowMax,
                    lineColor,
                    4f * scale,
                    ImDrawFlags.None,
                    lineThickness);
            }

            var textX = origin.X + 8f * scale;
            var icon = ActionLookup.GetIconWrap(item.Cue.ActionId);
            if (icon != null)
            {
                var iconSize = 24f * scale;
                var iconPos = new Vector2(origin.X + 4f * scale, y + 3f * scale);
                drawList.AddImage(icon.Handle, iconPos, iconPos + new Vector2(iconSize, iconSize));

                                if (showLine)
                {
                    drawList.AddRect(
                        iconPos,
                        iconPos + new Vector2(iconSize, iconSize),
                        lineColor,
                        2f * scale,
                        ImDrawFlags.None,
                        Math.Max(1f * scale, lineThickness * 0.85f));
                }

                textX = origin.X + 34f * scale;
            }

            var targetIcon = CueTargetCatalog.GetIconWrap(item.Cue);
            if (targetIcon != null)
            {
                var targetSize = 20f * scale;
                var targetPos = new Vector2(textX, y + 5f * scale);
                drawList.AddImage(
                    targetIcon.Handle,
                    targetPos,
                    targetPos + new Vector2(targetSize, targetSize));
                textX += targetSize + 4f * scale;
            }

            var text = item.IsPostHighlight
                ? I18n.Format(
                    "overlay.cue_row_now",
                    I18n.Get("overlay.now"),
                    label,
                    item.HighlightRemainingSec)
                : I18n.Format("overlay.cue_row", item.RemainingSeconds, label);
            DrawScaledTimelineText(
                new Vector2(textX, y + 6f * scale),
                ImGui.ColorConvertFloat4ToU32(color),
                text);

            y += CueRowHeight + CueRowGap;
        }

        return upcoming.Count == 0 ? origin.Y : y - CueRowGap;
    }
}
