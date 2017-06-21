# GujianCodeCollection
自动从淘号池内搜索可用古剑ol激活码


可自行编译代码。

或者

GujianCodeCollection/bin/Release/GujianCodeCollection.exe.config内填写要激活帐号的Cookie。

Cookie到激活页面 http://gjol.yy.com/jihuo.html

浏览器F12进入开发者模式。
------------------------------------------------------------------------
切换到Network/网络选项卡

随便填个激活码提交

选中新出现的那个激活码提交请求，右侧Headers选项卡内有Cookies项，完整复制

-------------------------------------------------------------------------
获取Cookie简单方法：

激活帐号页登录后，浏览器F12开发者模式

选择Console/控制台页面

粘贴copy(document.cookie)，回车

复制到的信息就是你的Cookie

--------------------------------------------------------------------------
<add key="COOKIES" value="cookie"/> 将value="cookie"内的cookie替换为自己帐号的cookie就可以了
执行GujianCodeCollection.exe开始自动淘号。
