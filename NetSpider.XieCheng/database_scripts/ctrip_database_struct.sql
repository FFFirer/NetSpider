CREATE DATABASE  IF NOT EXISTS `ctrip` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `ctrip`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 120.92.138.22    Database: ctrip
-- ------------------------------------------------------
-- Server version	8.0.20

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `airlines`
--

DROP TABLE IF EXISTS `airlines`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `airlines` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `AirlineCode` varchar(45) DEFAULT NULL,
  `AirlineName` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `airports`
--

DROP TABLE IF EXISTS `airports`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `airports` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `CityTlc` varchar(45) DEFAULT NULL,
  `CityName` varchar(45) DEFAULT NULL,
  `AirportTlc` varchar(45) DEFAULT NULL,
  `AirportName` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `cabins`
--

DROP TABLE IF EXISTS `cabins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cabins` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `LinkFlightId` bigint DEFAULT NULL,
  `FlightId` varchar(45) DEFAULT NULL,
  `CabinId` varchar(45) DEFAULT NULL,
  `Pid` varchar(255) DEFAULT NULL,
  `SaleType` varchar(45) DEFAULT NULL,
  `CabinClass` varchar(45) DEFAULT NULL,
  `PriceClass` varchar(45) DEFAULT NULL,
  `Price` varchar(45) DEFAULT NULL,
  `SalePrice` varchar(45) DEFAULT NULL,
  `PrintPrice` varchar(45) DEFAULT NULL,
  `FdPrice` varchar(45) DEFAULT NULL,
  `Rate` varchar(45) DEFAULT NULL,
  `MealType` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=199 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `characteristics`
--

DROP TABLE IF EXISTS `characteristics`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `characteristics` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `LinkedFlightId` bigint DEFAULT NULL,
  `FlightId` varchar(45) DEFAULT NULL,
  `LowestPrice` varchar(45) DEFAULT NULL,
  `LowestPriceId` varchar(45) DEFAULT NULL,
  `LowestCfPrice` varchar(45) DEFAULT NULL,
  `LowestChildPrice` varchar(45) DEFAULT NULL,
  `LowestChildCfPrice` varchar(45) DEFAULT NULL,
  `LowestChildAdultPrice` varchar(45) DEFAULT NULL,
  `LowestChildAdultCfPrice` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=58 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `flights`
--

DROP TABLE IF EXISTS `flights`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `flights` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `TimeTicks` bigint DEFAULT NULL,
  `FlightId` varchar(45) DEFAULT NULL,
  `FlightNumber` varchar(45) DEFAULT NULL,
  `SharedFlightNumber` varchar(45) DEFAULT NULL,
  `SharedFlightName` varchar(45) DEFAULT NULL,
  `AirlineCode` varchar(45) DEFAULT NULL,
  `AirlineName` varchar(45) DEFAULT NULL,
  `CraftTypeCode` varchar(45) DEFAULT NULL,
  `CraftKind` varchar(45) DEFAULT NULL,
  `CraftTypeName` varchar(45) DEFAULT NULL,
  `CraftTypeKindDeisplayName` varchar(45) DEFAULT NULL,
  `departureCityTlc` varchar(45) DEFAULT NULL,
  `departureCityName` varchar(45) DEFAULT NULL,
  `departureAirportTlc` varchar(45) DEFAULT NULL,
  `departureAirportName` varchar(45) DEFAULT NULL,
  `departureTerminalId` varchar(45) DEFAULT NULL,
  `departureTerminalName` varchar(45) DEFAULT NULL,
  `departureTerminalShortName` varchar(45) DEFAULT NULL,
  `departureDate` varchar(45) DEFAULT NULL,
  `arrivalCityTlc` varchar(45) DEFAULT NULL,
  `arrivalCityName` varchar(45) DEFAULT NULL,
  `arrivalAirportTlc` varchar(45) DEFAULT NULL,
  `arrivalAirportName` varchar(45) DEFAULT NULL,
  `arrivalTerminalId` varchar(45) DEFAULT NULL,
  `arrivalTerminalName` varchar(45) DEFAULT NULL,
  `arrivalTerminalShortName` varchar(45) DEFAULT NULL,
  `arrivalDate` varchar(45) DEFAULT NULL,
  `PunctualityRate` varchar(45) DEFAULT NULL,
  `MealType` varchar(45) DEFAULT NULL,
  `MealFlag` varchar(45) DEFAULT NULL,
  `OilFee` varchar(45) DEFAULT NULL,
  `Tax` varchar(45) DEFAULT NULL,
  `DurationDays` varchar(45) DEFAULT NULL,
  `StopTimes` varchar(45) DEFAULT NULL,
  `StopInfo` text,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=58 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping events for database 'ctrip'
--

--
-- Dumping routines for database 'ctrip'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-06-02 10:49:43
