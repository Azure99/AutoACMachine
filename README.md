# AutoACMachine
易于扩展的真*自动AC机(逃<br>
# 仅供娱乐

目前支持HDU(马上会支持其他OJ以及更多爬取网站)

特性:<br>
1.高正确率<br>
2.完整流程显示<br>
3.易于操作<br>
4.支持保存AC代码<br>

# 如何自己适配其他OJ？
请参考HDUClient实现IOnlineJudgeClient接口，在构造Controller时传入该实例。<br>
自己实现爬虫同理，实现ICrawler接口
