-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 25, 2025 at 05:06 PM
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
-- Table structure for table `transaction`
--

CREATE TABLE `transaction` (
  `transaction_id` int(11) NOT NULL,
  `plate_number` varchar(20) NOT NULL,
  `vehicle_type` enum('2 wheels','4 wheels') NOT NULL,
  `customer_type` enum('Guest','Staff','Services') NOT NULL,
  `floor` varchar(10) NOT NULL,
  `parking_zone` varchar(10) NOT NULL,
  `checkin_time` datetime DEFAULT current_timestamp(),
  `checked_in_by` int(11) DEFAULT NULL,
  `checkout_time` datetime DEFAULT NULL,
  `checked_out_by` varchar(50) DEFAULT NULL,
  `is_paid` enum('yes','no') DEFAULT 'no'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `transaction`
--

INSERT INTO `transaction` (`transaction_id`, `plate_number`, `vehicle_type`, `customer_type`, `floor`, `parking_zone`, `checkin_time`, `checked_in_by`, `checkout_time`, `checked_out_by`, `is_paid`) VALUES
(6, 'ABC123', '4 wheels', 'Guest', 'B1', 'A1', '2025-05-25 03:23:49', 10, NULL, NULL, 'no'),
(7, 'GGW321', '2 wheels', 'Staff', 'B2', 'A4', '2025-05-25 03:24:12', 10, NULL, NULL, 'no'),
(8, 'FAS632', '2 wheels', 'Guest', 'B1', 'A3', '2025-05-25 03:31:20', 11, NULL, NULL, 'no'),
(9, 'ZZZ354', '4 wheels', 'Staff', 'B1', 'A3', '2025-05-25 13:22:18', 10, NULL, NULL, 'no'),
(10, 'PAN431', '2 wheels', 'Services', 'B1', 'A2', '2025-05-25 13:23:26', 10, NULL, NULL, 'no'),
(11, 'UUU416', '4 wheels', 'Guest', 'B2', 'A5', '2025-05-25 13:26:54', 11, NULL, NULL, 'no'),
(12, 'JEN135', '2 wheels', 'Staff', 'B2', 'A6', '2025-05-25 13:27:05', 11, NULL, NULL, 'no'),
(13, 'KKK412', '4 wheels', 'Services', 'B2', 'A5', '2025-05-25 13:46:17', 10, NULL, NULL, 'no'),
(14, 'WEW888', '4 wheels', 'Guest', 'B1', 'A2', '2025-05-25 13:59:08', 11, NULL, NULL, 'no'),
(15, 'LOL551', '2 wheels', 'Guest', 'B1', 'A3', '2025-05-25 13:59:41', 11, NULL, NULL, 'no'),
(16, 'LEL913', '4 wheels', 'Services', 'B1', 'A3', '2025-05-25 14:35:46', 11, NULL, NULL, 'no'),
(17, 'POO991', '4 wheels', 'Staff', 'B1', 'A2', '2025-05-25 15:06:29', 10, NULL, NULL, 'no'),
(18, 'MEN091', '4 wheels', 'Staff', 'B2', 'A4', '2025-05-25 15:09:44', 10, NULL, NULL, 'no');

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
(11, 'Nicoloco', 'staff1', 'Nicolas Nadapa', 'staff');

--
-- Indexes for dumped tables
--

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
-- AUTO_INCREMENT for table `transaction`
--
ALTER TABLE `transaction`
  MODIFY `transaction_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `userID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

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
