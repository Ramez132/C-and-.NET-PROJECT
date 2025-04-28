namespace GameEngine
{
    public class Move
    {
        public (int m_Row, int m_Column) Source { get; set; }

        public (int m_Row, int m_Column) Target { get; set; }

        public bool IsThereCapture { get; set; }

        public (int m_capturedRow, int m_capturedColumn) CapturedSpot { get; set; }

        public Move((int sourceRow, int SourceColumn) i_Source, (int targetRow, int targetColumns) i_Target, bool isThereCapture = false, (int capturedRow, int capturedColumn) capturedSpot = default)
        {
            Source = i_Source;
            Target = i_Target;
            IsThereCapture = isThereCapture;
            CapturedSpot = capturedSpot;
        }

        public override string ToString()
        {
            char sourceRow = default;
            char sourceColumn = default;
            char targetRow = default;
            char targetColumn = default;

            if (!IsThereCapture)
            {
                sourceRow = (char)('A' + Source.m_Row);
                sourceColumn = (char)('a' + Source.m_Column);
                targetRow = (char)('A' + Target.m_Row);
                targetColumn = (char)('a' + Target.m_Column);
            }

            else
            {
                sourceRow = (char)('A' + Source.m_Row);
                sourceColumn = (char)('a' + Source.m_Column);
                targetRow = (char)('A' + CapturedSpot.m_capturedRow);
                targetColumn = (char)('a' + CapturedSpot.m_capturedColumn);
            }

            return $"{sourceRow}{sourceColumn}>{targetRow}{targetColumn}";
        }
    }
}
