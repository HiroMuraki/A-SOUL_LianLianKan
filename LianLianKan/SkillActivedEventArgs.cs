using System;

namespace LianLianKan {
    public class SkillActivedEventArgs : EventArgs {
        private LLKSkill _skill;
        public LLKSkill Skill {
            get {
                return _skill;
            }
        }

        public SkillActivedEventArgs(LLKSkill skill) {
            _skill = skill;
        }
    }
}
