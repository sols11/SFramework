## UI框架

支持栈缓存，智能层级控制，模态窗体管理等功能。将UI框架融入到SFramework这个大框架中，使用外观模式，专门管理游戏中的UI，可以非常方便的通过接口调用打开关闭UI。

##### Common 框架核心参数 
UI枚举类型+路径常量

定义系统三个枚举类型，这3个枚举类型将UI设置分好类，位置，显示，遮罩

**UIFormType** 窗体类型

Normal，Fixed，Popup

**UIFormShowMode**显示类型

Normal,ReverseChange,HideOther

**UIFormLucenyType** 透明度类型

Lucency,Translucence,ImPenetrable,Pentrate

##### ViewBase UI继承的基类

设置3个枚举的值

提供4种生命周期状态供外界调用

- Display
- Hiding
- Redisplay
- Freeze

Popup的生命周期状态调用与其他相比要多调用一个遮罩

封装常用的UI事件方法

- ShowText 多语言本地化显示Text
- OpenUIForm 调用UIManager打开UI窗体
- CloseUIForm
 
##### UIManager UI的管理者
管理游戏中的所有UI窗体，智能控制各UI的层级，生命周期，实现栈存储

属性-CanvasGO,UICamera,UIMaskMgr

3个字典缓存 窗体预设路径，所有UI，当前显示的UI

方法-ShowUIForms，CloseUIForms

##### UIMaskMgr 负责“弹出窗体”模态显示实现
UIMaskMgr是为UIManager所使用的，不需要显式调用

方法-SetMaskWindow，CancelMaskWindow

现将UIMgr和UIMaskMgr相互引用，一同赋值给各个UI

## UI框架的使用
1.  创建Prefab
1.  将路径加入UIMgr字典集中管理
1.  创建UI脚本，继承ViewBase，使用SFramework命名空间，放在DreamKeeper命名空间下，设置
1. 写好button等的事件，调用UIMgr的方法ShowUIForms和CloseUIForms来开关UI
1. 这样我们每次只需要在当前UI脚本中写好要调用的方法和UI名称，而不用管UI对象是什么类型，就可以实现UI切换了（底层由UIMgr自行管理）

