-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 27, 2025 at 04:05 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `pslmv2`
--

-- --------------------------------------------------------

--
-- Table structure for table `monthly_access_pass`
--

CREATE TABLE `monthly_access_pass` (
  `pass_id` int(11) NOT NULL,
  `plate_number` varchar(20) NOT NULL,
  `start_date` date NOT NULL,
  `end_date` date NOT NULL,
  `issued_by` varchar(50) NOT NULL,
  `status` enum('Active','Expired') DEFAULT 'Active'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `monthly_access_pass`
--

INSERT INTO `monthly_access_pass` (`pass_id`, `plate_number`, `start_date`, `end_date`, `issued_by`, `status`) VALUES
(1, 'GGW810', '2025-05-27', '2025-06-26', '12', 'Active'),
(2, 'RNG551', '2025-05-27', '2025-06-26', '10', 'Active'),
(3, 'XCA611', '2025-05-27', '2025-04-26', '12', 'Active'),
(4, 'POT512', '2025-05-27', '2025-06-26', '10', 'Active'),
(5, 'KAN512', '2025-05-27', '2025-06-26', '12', 'Active'),
(6, 'POA412', '2025-05-27', '2025-06-26', '12', 'Active');

-- --------------------------------------------------------

--
-- Table structure for table `transaction`
--

CREATE TABLE `transaction` (
  `transaction_id` int(11) NOT NULL,
  `plate_number` varchar(20) NOT NULL,
  `vehicle_type` enum('2 wheels','4 wheels') NOT NULL,
  `customer_type` enum('Guest','Staff','Services','Visitors') NOT NULL,
  `floor` varchar(10) NOT NULL,
  `parking_zone` varchar(10) NOT NULL,
  `checkin_time` datetime DEFAULT current_timestamp(),
  `checked_in_by` int(11) DEFAULT NULL,
  `checkout_time` datetime DEFAULT NULL,
  `checked_out_by` varchar(50) DEFAULT NULL,
  `is_paid` enum('yes','no') DEFAULT 'no',
  `fee` int(11) DEFAULT 0,
  `is_pass_user` enum('true','false') DEFAULT 'false'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `transaction`
--

INSERT INTO `transaction` (`transaction_id`, `plate_number`, `vehicle_type`, `customer_type`, `floor`, `parking_zone`, `checkin_time`, `checked_in_by`, `checkout_time`, `checked_out_by`, `is_paid`, `fee`, `is_pass_user`) VALUES
(6, 'ABC123', '4 wheels', 'Guest', 'B1', 'A1', '2025-05-25 03:23:49', 10, NULL, NULL, 'yes', 0, 'false'),
(7, 'GGW321', '2 wheels', 'Staff', 'B2', 'A4', '2025-05-25 03:24:12', 10, '2025-05-27 01:37:08', NULL, 'yes', 0, 'false'),
(8, 'FAS632', '2 wheels', 'Guest', 'B1', 'A3', '2025-05-25 03:31:20', 11, '2025-05-27 02:12:56', '10', 'yes', 0, 'false'),
(9, 'ZZZ354', '4 wheels', 'Staff', 'B1', 'A3', '2025-05-25 13:22:18', 10, NULL, NULL, 'yes', 0, 'false'),
(10, 'PAN431', '2 wheels', 'Services', 'B1', 'A2', '2025-05-25 13:23:26', 10, NULL, NULL, 'yes', 0, 'false'),
(11, 'UUU416', '4 wheels', 'Guest', 'B2', 'A5', '2025-05-25 13:26:54', 11, '2025-05-27 01:44:13', '10', 'yes', 0, 'false'),
(12, 'JEN135', '2 wheels', 'Staff', 'B2', 'A6', '2025-05-25 13:27:05', 11, NULL, NULL, 'yes', 0, 'false'),
(13, 'KKK412', '4 wheels', 'Services', 'B2', 'A5', '2025-05-25 13:46:17', 10, NULL, NULL, 'yes', 0, 'false'),
(14, 'WEW888', '4 wheels', 'Guest', 'B1', 'A2', '2025-05-25 13:59:08', 11, NULL, NULL, 'yes', 0, 'false'),
(15, 'LOL551', '2 wheels', 'Guest', 'B1', 'A3', '2025-05-25 13:59:41', 11, NULL, NULL, 'yes', 0, 'false'),
(16, 'LEL913', '4 wheels', 'Services', 'B1', 'A3', '2025-05-25 14:35:46', 11, NULL, NULL, 'yes', 0, 'false'),
(17, 'POO991', '4 wheels', 'Staff', 'B1', 'A2', '2025-05-25 15:06:29', 10, NULL, NULL, 'yes', 0, 'false'),
(18, 'MEN091', '4 wheels', 'Staff', 'B2', 'A4', '2025-05-25 15:09:44', 10, NULL, NULL, 'yes', 0, 'false'),
(19, 'IKA712', '4 wheels', 'Staff', 'B1', 'A2', '2025-05-26 19:53:57', 10, NULL, NULL, 'yes', 0, 'false'),
(20, 'IKA713', '2 wheels', 'Guest', 'B1', 'A3', '2025-05-26 19:58:51', 12, NULL, NULL, 'yes', 0, 'false'),
(21, 'REK651', '4 wheels', 'Guest', 'B1', 'A2', '2025-05-26 20:57:55', 10, NULL, NULL, 'yes', 0, 'false'),
(22, 'ABC978', '4 wheels', 'Visitors', 'B1', 'A3', '2025-05-26 23:20:35', 10, '2025-05-27 02:09:38', '10', 'yes', 90, 'false'),
(23, 'NVM235', '2 wheels', 'Visitors', 'B1', 'A2', '2025-05-26 23:20:52', 10, '2025-05-27 02:10:46', '10', 'yes', 90, 'false'),
(24, 'GGW810', '4 wheels', 'Visitors', 'B1', 'A3', '2025-05-27 00:02:37', 10, NULL, NULL, 'no', 0, 'false'),
(25, 'RNG551', '4 wheels', 'Visitors', 'B1', 'A1', '2025-05-27 01:44:55', 10, '2025-05-27 19:07:25', '12', 'yes', 0, 'true'),
(26, 'GNR', '2 wheels', 'Staff', 'B2', 'A4', '2025-05-27 01:45:05', 10, NULL, NULL, 'yes', 0, 'false'),
(27, 'BAD696', '2 wheels', 'Services', 'B2', 'A5', '2025-05-27 01:46:33', 10, NULL, NULL, 'yes', 0, 'false'),
(28, 'LEL911', '4 wheels', 'Guest', 'B1', 'A3', '2025-05-27 02:08:58', 10, NULL, NULL, 'yes', 0, 'false'),
(29, 'XCX182', '4 wheels', 'Visitors', 'B1', 'A2', '2025-05-27 02:09:16', 10, NULL, NULL, 'no', 0, 'false'),
(30, 'NVA612', '4 wheels', 'Staff', 'B1', 'A1', '2025-05-27 02:12:07', 10, NULL, NULL, 'yes', 0, 'false'),
(31, 'NZX', '2 wheels', 'Visitors', 'B1', 'A2', '2025-05-27 02:12:17', 10, NULL, NULL, 'no', 0, 'false'),
(32, 'XCA611', '4 wheels', 'Visitors', 'B1', 'A1', '2025-05-27 02:12:37', 10, '2025-05-27 17:20:14', '12', 'yes', 0, 'true'),
(33, 'LIG321', '4 wheels', 'Visitors', 'B1', 'A1', '2025-05-27 14:54:37', 12, NULL, NULL, 'no', 0, 'false'),
(34, 'POT512', '4 wheels', 'Visitors', 'B1', 'A2', '2025-05-27 14:54:56', 12, NULL, NULL, 'no', 0, 'false'),
(35, 'ABK311', '2 wheels', 'Visitors', 'B1', 'A3', '2025-05-27 14:55:08', 12, NULL, NULL, 'no', 0, 'false'),
(36, 'RIC999', '2 wheels', 'Guest', 'B2', 'A4', '2025-05-27 14:55:22', 12, '2025-05-27 19:07:46', '12', 'yes', 0, 'false'),
(37, 'NNN312', '2 wheels', 'Visitors', 'B1', 'A2', '2025-05-27 15:27:25', 10, NULL, NULL, 'no', 0, 'false'),
(38, 'ACK231', '4 wheels', 'Visitors', 'B2', 'A5', '2025-05-27 17:01:01', 12, NULL, NULL, 'no', 0, 'false'),
(39, 'ZEF512', '4 wheels', 'Services', 'B1', 'A2', '2025-05-27 17:01:12', 12, NULL, NULL, 'yes', 0, 'false'),
(40, 'KAN512', '2 wheels', 'Visitors', 'B2', 'A4', '2025-05-27 17:01:25', 12, '2025-05-27 19:58:11', '12', 'yes', 0, 'true'),
(41, 'POA412', '4 wheels', 'Visitors', 'B1', 'A2', '2025-05-27 19:05:18', 12, NULL, NULL, 'no', 0, 'false'),
(42, 'PAG516', '4 wheels', 'Visitors', 'B1', 'A1', '2025-05-27 19:57:12', 12, NULL, NULL, 'no', 0, 'false'),
(43, 'XCA611', '4 wheels', 'Visitors', 'B1', 'A3', '2025-05-27 20:13:05', 12, '2025-05-27 20:13:23', '12', 'yes', 30, 'false');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `userID` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password_hash` varchar(255) NOT NULL,
  `fullname` varchar(50) NOT NULL,
  `role` enum('admin','staff') NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`userID`, `username`, `password_hash`, `fullname`, `role`) VALUES
(10, 'RicaLoca', 'admin1', 'Rica Nience', 'admin'),
(11, 'Nicoloco', 'staff1', 'Nicolas Nadapa', 'staff'),
(12, 'RomanovS', 'staff2', 'Romanov Regilos', 'staff');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `monthly_access_pass`
--
ALTER TABLE `monthly_access_pass`
  ADD PRIMARY KEY (`pass_id`);

--
-- Indexes for table `transaction`
--
ALTER TABLE `transaction`
  ADD PRIMARY KEY (`transaction_id`),
  ADD KEY `checked_in_by` (`checked_in_by`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`userID`),
  ADD UNIQUE KEY `username` (`username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `monthly_access_pass`
--
ALTER TABLE `monthly_access_pass`
  MODIFY `pass_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `transaction`
--
ALTER TABLE `transaction`
  MODIFY `transaction_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=44;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `userID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `transaction`
--
ALTER TABLE `transaction`
  ADD CONSTRAINT `transaction_ibfk_1` FOREIGN KEY (`checked_in_by`) REFERENCES `users` (`userID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
