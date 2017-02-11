-- phpMyAdmin SQL Dump
-- version 4.3.11
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Feb 11, 2017 at 11:28 AM
-- Server version: 5.6.24
-- PHP Version: 5.6.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `newerastore`
--

-- --------------------------------------------------------

--
-- Table structure for table `categories`
--

CREATE TABLE IF NOT EXISTS `categories` (
  `category_id` int(11) NOT NULL,
  `category_name` varchar(100) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `categories`
--

INSERT INTO `categories` (`category_id`, `category_name`) VALUES
(11, 'Can Goods'),
(12, 'Frozen Foods');

-- --------------------------------------------------------

--
-- Table structure for table `categories_subcategories`
--

CREATE TABLE IF NOT EXISTS `categories_subcategories` (
  `ID` int(11) NOT NULL,
  `category_id` int(11) NOT NULL,
  `subcategory_id` int(11) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `categories_subcategories`
--

INSERT INTO `categories_subcategories` (`ID`, `category_id`, `subcategory_id`) VALUES
(15, 11, 17),
(16, 11, 18),
(21, 11, 23),
(22, 12, 24);

-- --------------------------------------------------------

--
-- Table structure for table `products`
--

CREATE TABLE IF NOT EXISTS `products` (
  `product_id` int(11) NOT NULL,
  `product_code` varchar(50) NOT NULL,
  `product_description` varchar(255) NOT NULL,
  `cost` double NOT NULL,
  `selling_price` double NOT NULL,
  `quantity` int(11) NOT NULL,
  `supplier_id` int(11) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `products`
--

INSERT INTO `products` (`product_id`, `product_code`, `product_description`, `cost`, `selling_price`, `quantity`, `supplier_id`) VALUES
(4, 'SLKDJF2', 'MODERN TUNA', 2500, 2750, 20, 5678),
(5, '234KK', 'Modern San marino', 2000, 2400, 20, 5678);

-- --------------------------------------------------------

--
-- Table structure for table `product_catsub`
--

CREATE TABLE IF NOT EXISTS `product_catsub` (
  `prod_catsub_id` int(11) NOT NULL,
  `product_id` int(11) NOT NULL,
  `ID` int(11) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `product_catsub`
--

INSERT INTO `product_catsub` (`prod_catsub_id`, `product_id`, `ID`) VALUES
(4, 4, 15),
(5, 5, 16);

-- --------------------------------------------------------

--
-- Table structure for table `subcategories`
--

CREATE TABLE IF NOT EXISTS `subcategories` (
  `subcategory_id` int(11) NOT NULL,
  `subcategory_name` varchar(100) NOT NULL,
  `markup` int(11) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `subcategories`
--

INSERT INTO `subcategories` (`subcategory_id`, `subcategory_name`, `markup`) VALUES
(17, '555 Tuna', 20),
(18, 'San Marino', 50),
(23, 'Century', 30),
(24, 'Hotdogs', 20);

-- --------------------------------------------------------

--
-- Table structure for table `suppliers`
--

CREATE TABLE IF NOT EXISTS `suppliers` (
  `supplier_id` varchar(50) NOT NULL,
  `supplier_name` varchar(100) NOT NULL,
  `supplier_address` varchar(255) NOT NULL,
  `contact_person` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `suppliers`
--

INSERT INTO `suppliers` (`supplier_id`, `supplier_name`, `supplier_address`, `contact_person`) VALUES
('5678', 'Kuroba Kaito', '#19 makabebe street', '09569477986');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE IF NOT EXISTS `users` (
  `user_id` int(11) NOT NULL,
  `user_name` varchar(50) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL,
  `role` varchar(50) NOT NULL,
  `vat` int(11) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`user_id`, `user_name`, `username`, `password`, `role`, `vat`) VALUES
(1, 'Kenneth Cutay Pingol', 'kenneth', '123456', 'Administrator', 12),
(2, 'Miles Sheldon', 'sheldon', 'sheldon', 'Cashier', 15);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`category_id`);

--
-- Indexes for table `categories_subcategories`
--
ALTER TABLE `categories_subcategories`
  ADD PRIMARY KEY (`ID`), ADD KEY `category_id` (`category_id`), ADD KEY `subcategory_id` (`subcategory_id`);

--
-- Indexes for table `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`product_id`);

--
-- Indexes for table `product_catsub`
--
ALTER TABLE `product_catsub`
  ADD PRIMARY KEY (`prod_catsub_id`), ADD KEY `product_id` (`product_id`), ADD KEY `ID` (`ID`);

--
-- Indexes for table `subcategories`
--
ALTER TABLE `subcategories`
  ADD PRIMARY KEY (`subcategory_id`);

--
-- Indexes for table `suppliers`
--
ALTER TABLE `suppliers`
  ADD PRIMARY KEY (`supplier_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `categories`
--
ALTER TABLE `categories`
  MODIFY `category_id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=13;
--
-- AUTO_INCREMENT for table `categories_subcategories`
--
ALTER TABLE `categories_subcategories`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=23;
--
-- AUTO_INCREMENT for table `products`
--
ALTER TABLE `products`
  MODIFY `product_id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=6;
--
-- AUTO_INCREMENT for table `product_catsub`
--
ALTER TABLE `product_catsub`
  MODIFY `prod_catsub_id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=6;
--
-- AUTO_INCREMENT for table `subcategories`
--
ALTER TABLE `subcategories`
  MODIFY `subcategory_id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=25;
--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=3;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `categories_subcategories`
--
ALTER TABLE `categories_subcategories`
ADD CONSTRAINT `categories_subcategories_ibfk_1` FOREIGN KEY (`category_id`) REFERENCES `categories` (`category_id`),
ADD CONSTRAINT `categories_subcategories_ibfk_2` FOREIGN KEY (`subcategory_id`) REFERENCES `subcategories` (`subcategory_id`);

--
-- Constraints for table `product_catsub`
--
ALTER TABLE `product_catsub`
ADD CONSTRAINT `product_catsub_ibfk_1` FOREIGN KEY (`product_id`) REFERENCES `products` (`product_id`),
ADD CONSTRAINT `product_catsub_ibfk_2` FOREIGN KEY (`ID`) REFERENCES `categories_subcategories` (`ID`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
