using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianLianKan {
    using CurrentTokenTypes = List<LLKTokenType>;
    using LLKTokens = IEnumerable<LLKToken>;
    using LLKTokenTypes = IEnumerable<LLKTokenType>;
    public interface IGame {
        #region 公开事件
        /// <summary>
        /// 游戏完成
        /// </summary>
        event EventHandler<GameCompletedEventArgs> GameCompleted;
        /// <summary>
        /// 布局信息被更改
        /// </summary>
        event EventHandler<LayoutResetedEventArgs> LayoutReseted;
        /// <summary>
        /// 技能启用
        /// </summary>
        event EventHandler<SkillActivedEventArgs> SkillActived;
        #endregion

        #region 公开属性
        /// <summary>
        /// 当前的Token类型
        /// </summary>
        CurrentTokenTypes CurrentTokenTypes { get; }
        /// <summary>
        /// 当前token序列
        /// </summary>
        LLKTokens LLKTokenArray { get; }
        /// <summary>
        /// 当前token类型序列
        /// </summary>
        LLKTokenTypes TokenTypeArray { get; }
        /// <summary>
        /// 行数
        /// </summary>
        int RowSize { get; }
        /// <summary>
        /// 列数
        /// </summary>
        int ColumnSize { get; }
        /// <summary>
        /// 游戏类型
        /// </summary>
        GameType GameType { get; }
        /// <summary>
        /// 技能点
        /// </summary>
        int SkillPoint { get; }
        #endregion

        #region 公开方法
        /// <summary>
        /// 开始游戏
        /// </summary>
        /// <param name="rowSize">行数</param>
        /// <param name="columnSize">列数</param>
        /// <param name="tokenAmount">token类型数</param>
        void StartGame(int rowSize, int columnSize, int tokenAmount);
        Task StartGameAsync(int rowSize, int columnSize, int tokenAmount);
        /// <summary>
        /// 恢复游戏
        /// </summary>
        /// <param name="restorePack">恢复信息</param>
        void RestoreGame(IGameRestore restorePack);
        Task RestoreGameAsync(IGameRestore restorePack);
        /// <summary>
        /// 选择token
        /// </summary>
        /// <param name="token">要选择的token</param>
        /// <returns>选择结果</returns>
        TokenSelectResult SelectToken(LLKToken token);
        Task<TokenSelectResult> SelectTokenAsync(LLKToken token);
        /// <summary>
        /// 启用技能
        /// </summary>
        /// <param name="skill">技能类型</param>
        void ActiveSkill(LLKSkill skill);
        Task ActiveSkillAsync(LLKSkill skill);
        #endregion
    }
}
