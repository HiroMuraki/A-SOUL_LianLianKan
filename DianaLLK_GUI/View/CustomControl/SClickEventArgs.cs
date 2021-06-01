// using MergeDiana.GameLib;
using LianLianKan;
using System;

namespace DianaLLK_GUI {
    public class SClickEventArgs : EventArgs {
        private LLKSkill _skill;

        public LLKSkill SKill {
            get {
                return _skill;
            }
        }

        public SClickEventArgs(LLKSkill skill) {
            _skill = skill;
        }
    }
}