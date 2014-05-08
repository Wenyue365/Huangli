-- phpMyAdmin SQL Dump
-- version 3.4.3.1
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Apr 30, 2014 at 06:52 AM
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
  `soloarDate_str` varchar(64) DEFAULT NULL,
  `lunarDate` varchar(64) DEFAULT NULL,
  `yearOrder` varchar(32) DEFAULT NULL,
  `zodiac` varchar(32) DEFAULT NULL,
  `monthOrder` varchar(32) DEFAULT NULL,
  `dayOrder` varchar(32) DEFAULT NULL,
  `birthGod` varchar(64) DEFAULT NULL,
  `fiveElem` varchar(32) DEFAULT NULL,
  `collide` varchar(64) DEFAULT NULL,
  `pengAvoid` varchar(64) DEFAULT NULL,
  `goodAngelYi` varchar(256) DEFAULT NULL,
  `evilAngelJi` varchar(256) DEFAULT NULL,
  `yi` varchar(256) DEFAULT NULL,
  `ji` varchar(256) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
