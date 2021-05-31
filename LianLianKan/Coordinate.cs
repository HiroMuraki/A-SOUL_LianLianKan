public struct Coordinate {
    private int _row;
    private int _column;

    public static readonly Coordinate NullCooriante = new Coordinate(-1, -1);
    public int Row {
        get {
            return _row;
        }
        set {
            _row = value;
        }
    }
    public int Column {
        get {
            return _column;
        }
        set {
            _column = value;
        }
    }

    public Coordinate(int row, int column) {
        _row = row;
        _column = column;
    }

    public static bool operator ==(Coordinate left, Coordinate right) {
        return left.Row == right.Row && left.Column == right.Column;
    }
    public static bool operator !=(Coordinate left, Coordinate right) {
        return !(left == right);
    }
    public override bool Equals(object obj) {
        return base.Equals(obj);
    }
    public override int GetHashCode() {
        return base.GetHashCode();
    }
    public override string ToString() {
        return $"[Row = {Row}, Column = {Column}]";
    }
}