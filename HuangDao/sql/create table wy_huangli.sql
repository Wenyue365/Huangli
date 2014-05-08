<?xml version="1.0" encoding="utf-16"?>
<TXHuangDaoDay xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <FID>4506</FID>
  <ShowTime>2014-01-11T00:00:00</ShowTime>
  <LunerDate>二○一三年十二月十一  癸巳年乙丑月壬午日</LunerDate>
  <Astro>摩羯座</Astro>
  <GoodToDo>沐浴 剃头 整手足甲 畋猎 捕捉</GoodToDo>
  <BadToDo>祈福 求嗣 上册受封 上官赴任 上表章 袭爵受封 临政亲民 冠带 出行 会亲友 安床 嫁娶 结婚姻 解除 进人口 纳采问名 裁衣 移徙 求医疗病 筑堤防 修造动土 竖柱上梁 修仓库 苫盖 经络 酝酿 开仓库出货物 开市 立券交易 纳财 开渠穿井 修置产室 取鱼 乘船渡水 栽种 牧养 纳畜 破土 启攒 安葬</BadToDo>
</TXHuangDaoDay>

CREATE TABLE wy_huangli(
fid VARCHAR(32), 
showtime VARCHAR(32), 
lunerdate VARCHAR(32),
goodtodo VARCHAR(256),
badtodo VARCHAR(256)
);

insert into wy_huangli(fid, showtime, lunerdate, goodtodo, badtodo)
values('{0}', '{1}', '{2}', '{3}', '{4}');
