using System;

namespace LianLianKan {
    public class SkillActivedEventArgs : EventArgs {
        private LLKSkill _skill;
        private bool _activeResult;

        public LLKSkill Skill {
            get {
                return _skill;
            }
        }
        public bool ActiveResult {
            get {
                return _activeResult;
            }
        }

        public SkillActivedEventArgs(LLKSkill skill, bool activeResult) {
            _skill = skill;
            _activeResult = activeResult;
        }
    }
}
