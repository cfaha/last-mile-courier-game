# Last Mile Courier (安卓游戏)

休闲策略+路线规划手游《快递员：最后一公里》策划与原型仓库。

## 目录
- docs/GDD.md — 完整策划文档
- docs/LEVELS.md — 关卡/城市分区与难度曲线
- docs/LEVEL_BALANCE.md — 关卡平衡表
- docs/UI_WIREFRAMES.md — 页面结构与交互说明
- docs/ROADMAP.md — 6周开发里程碑
- docs/ASSET_SLOTS.md — 资源位说明

## 核心玩法一句话
在有限时间内规划最优配送路线，送完更多订单并维持满意度。

## 当前完成度
- 玩法流程骨架（规划→配送→结算）
- 关卡配置/区域权重/强制事件
- 事件弹窗与教程遮罩
- 订单排序（拖拽+上下按钮）
- 结算面板（评分/金币/耗时）
- 任务/货币/商店基础系统
- 事件/订单权重配置化

## 配置文件
- config/levels.json — 关卡配置
- config/events.json — 事件权重
- config/order_weights.json — 订单权重
- config/shop.json — 商店物品

## 技术建议
- 引擎：Unity
- 平台：安卓优先
- 联网：单机为主（可选排行/云存档）
