-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: Feb 22, 2023 at 01:49 PM
-- Server version: 10.4.21-MariaDB
-- PHP Version: 8.1.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `MatsalApplikation`
--

-- --------------------------------------------------------

--
-- Table structure for table `LunchScans`
--

CREATE TABLE `LunchScans` (
  `Date` datetime NOT NULL DEFAULT current_timestamp(),
  `Barcode` varchar(255) NOT NULL,
  `ScanCode` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Table structure for table `Persons`
--

CREATE TABLE `Persons` (
  `FullName` varchar(255) NOT NULL,
  `Barcode` varchar(255) NOT NULL,
  `Privilege` smallint(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `Persons`
--

INSERT INTO `Persons` (`FullName`, `Barcode`, `Privilege`) VALUES
('John Doe', 'SOD14090', 1),
('Jane Doe', 'SOD19050', 0); -- 1 means that you are a teacher meanwhile 0 means you are a student


--
-- Indexes for dumped tables
--

--
-- Indexes for table `LunchScans`
--
ALTER TABLE `LunchScans`
  ADD KEY `barcode` (`Barcode`);

--
-- Indexes for table `Persons`
--
ALTER TABLE `Persons`
  ADD UNIQUE KEY `Barcode` (`Barcode`);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `LunchScans`
--
ALTER TABLE `LunchScans`
  ADD CONSTRAINT `barcode` FOREIGN KEY (`Barcode`) REFERENCES `Persons` (`Barcode`);
COMMIT;