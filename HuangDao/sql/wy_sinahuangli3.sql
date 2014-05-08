-- phpMyAdmin SQL Dump
-- version 3.4.3.1
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: May 02, 2014 at 01:57 PM
-- Server version: 5.0.18
-- PHP Version: 5.2.17

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `wenyue365`
--

-- --------------------------------------------------------

--
-- Table structure for table `wy_sinahuangli`
--

CREATE TABLE IF NOT EXISTS `wy_sinahuangli` (
  `solarDate` date NOT NULL,
  `soloarDate_str` varchar(64) default NULL,
  `lunarDate` varchar(64) default NULL,
  `yearOrder` varchar(32) default NULL,
  `zodiac` varchar(32) default NULL,
  `monthOrder` varchar(32) default NULL,
  `dayOrder` varchar(32) default NULL,
  `birthGod` varchar(64) default NULL,
  `fiveElem` varchar(32) default NULL,
  `collide` varchar(64) default NULL,
  `pengAvoid` varchar(64) default NULL,
  `goodAngelYi` varchar(256) default NULL,
  `evilAngelJi` varchar(256) default NULL,
  `yi` varchar(256) default NULL,
  `ji` varchar(256) default NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `wy_sinahuangli`
--

INSERT INTO `wy_sinahuangli` (`solarDate`, `soloarDate_str`, `lunarDate`, `yearOrder`, `zodiac`, `monthOrder`, `dayOrder`, `birthGod`, `fiveElem`, `collide`, `pengAvoid`, `goodAngelYi`, `evilAngelJi`, `yi`, `ji`) VALUES
('2004-01-01', '公元2004年01月01日', '农历12月(大)10日', '癸未年', '生肖属羊', '甲子月', '己卯日', '占门厕外正南', '城头土　平执位', '冲鸡(癸酉)煞西', '己不破券二比并亡　卯不穿井水泉不香', '天恩民日不将玉堂五合', '死神　月刑　天吏　天贼', '宜：祭祀　平治道涂　修坟　除服成服　余事勿取', '移徙　入宅　嫁娶　掘井　安葬');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
