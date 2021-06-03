namespace LianLianKan {
    public abstract class LLKTokenBase {
        private object _content;

        public object Content {
            get {
                return _content;
            }
            set {
                _content = value;
            }
        }

        public LLKTokenBase() {

        }
    }
}
