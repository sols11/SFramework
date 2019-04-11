# SFramework
基于Unity引擎的万能游戏框架 Unity-Framework

- 独立设计开发的原创游戏框架，具备良好的可扩展性
- 控制游戏生命周期，框架尽量不继承Monobehavior
- 基于单例模式，外观模式，桥接模式等设计模式
- 可使用PhysX物理引擎，动画帧事件等基于Monobehavior的功能
- 代码追求精简高效，核心代码仅5000+行

![游戏循环](http://on-img.com/chart_image/5982b660e4b0e56e5d06455c.png)

## 项目工程结构

Plugins：插件

Repo：资源

Resources：热加载资源

Scenes：场景

Scripts：脚本

StreamingAssets：热加载配置文件

## 命名规范

类名、方法、属性	大写开头

字段名、参数、局部变量 小写开头

每个脚本文件头需要填写：
```c#
/*----------------------------------------------------------------------------
Author:
    Anotts
Date:
    2017/08/01
Description:
    简介：
    作用：
    使用：
    补充：
History:
----------------------------------------------------------------------------*/
```

现已将各脚本的说明写入文件头，可直接查阅学习使用。

## 安装集成

建议直接以此项目为基础进行开发。

如果要集成到其他项目，可以直接Clone到项目目录下，也可以通过安装unitypackage包的形式集成。

尽量不要让框架本身和项目产生耦合，框架的命名空间是SFramework，项目的命名空间默认是ProjectScript，需要区分开。

项目开发时往往可能需要对框架进行修改，这时只需要开一个branch，将修改提交到branch，最后pull request到框架原来的Git项目（即SFramewok）即可。

## 快速上手

可从Start场景开始开发，可在Doc文件夹中查看技术文档。

1. 添加场景只需按SceneStateController类的说明来做
2. 添加角色只需按角色架构文档说明来做
3. 添加UI只需按UIManager类的说明来做
4. 添加音乐音效只需按AudioMgr类的说明来做
5. 存档设置只需按GameDataMgr类的说明来做
6. 添加系统只需按GameMainProgram类的说明来做
7. 以此类推，待补充……

## 开源协议

GNU General Public License v3.0 （GPL） 