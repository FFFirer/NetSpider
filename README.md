# NetSpider

## 简介

使用 dotnet core编写的一个不完善的爬虫框架。使注意力集中于爬取规则的编写和数据的存取。例子全部存储于NetSpider.Core.ConsoleTest下。

## 基本组成

主要由下载，抓取，存储三部分组成。

## 基本逻辑

下载负责完成所有目标请求，然后交由数据分析器来解析数据；数据解析器用来负责解析所有请求完成的页面，分析出新的链接交由下载器继续下载新的页面，分析出要抓取的数据则交给存储器进行保存；保存器是将抓取到的数据进行持久化保存的地方，可以自己编写要保存的处理逻辑。

抓取逻辑，保存逻辑，数据模型，需要自己手动编写加载到爬虫中。

## 计划支持

消息队列 ❌  
IP代理 ❌  
爬虫速度控制 ❌  
MongoDB ❌  
分布式/微服务 ❌