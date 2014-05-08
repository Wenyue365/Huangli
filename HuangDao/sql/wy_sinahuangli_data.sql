-- phpMyAdmin SQL Dump
-- version 3.4.3.1
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Apr 30, 2014 at 03:55 PM
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

INSERT INTO `wy_sinahuangli` (`{0}`, `{1}`, `{2}`, `{3}`, `{4}`, `{5}`, `{6}`, `{7}`, `{8}`, `{9}`, `{10}`, `{11}`, `{12}`, `{13}`, `{14}`) VALUES
(shl.m_solarDate, shl.m_soloarDate_str, shl.m_lunarDate, shl.m_yearOrder, shl.m_zodiac, shl.m_monthOrder, shl.m_dayOrder, shl.m_birthGod, shl.m_fiveElem, shl.m_collide, shl.m_pengAvoid, shl.m_goodAngelYi, shl.m_evilAngelJi, shl.m_yi, shl.m_ji);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
