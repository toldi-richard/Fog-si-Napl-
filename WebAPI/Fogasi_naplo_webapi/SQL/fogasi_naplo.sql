-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2022. Már 22. 13:11
-- Kiszolgáló verziója: 10.4.22-MariaDB
-- PHP verzió: 8.1.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `fogasi_naplo`
--
CREATE DATABASE IF NOT EXISTS `fogasi_naplo` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `fogasi_naplo`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `felhasznalok`
--

CREATE TABLE `felhasznalok` (
  `azonosito` int(11) NOT NULL,
  `szerepkorID` int(11) NOT NULL,
  `jelszo` varchar(70) NOT NULL,
  `email_cim` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- A tábla adatainak kiíratása `felhasznalok`
--

INSERT INTO `felhasznalok` (`azonosito`, `szerepkorID`, `jelszo`, `email_cim`) VALUES
(1000, 2, 'halor1987', 'halor1987@gmail.com'),
(101010, 1, '$2a$11$qnfhvMa.ONt0LsJJ2Vh0kefY/v5SXY0GpxDnKrblqSDDpBekbP1TO', 'admin@gmail.com'),
(111111, 3, '$2a$11$Mj7LAwxcw2SXrhBbUvkfT.MXPbJKwCUmrh1R0vlr7PZ.ffq4epLzS', 'Agoston1@gmail.com'),
(143299, 3, '$2a$11$sCJ87QuaoBbc6N4yRGigHOzEqnYyYA3OGKGY4jqQp8bpJ52Si20Uy', 'user12@gmail.com'),
(222222, 3, '$2a$11$FoK3kDz6Z/IuOdKZZznhvOqT9jvX.TlmKPJH2P9/f0SaBUvSnlTTS', 'Klarika22@gmail.com'),
(312955, 3, '$2a$11$EArp6riKs5g.wLJ2lDt6Web/0lemXOa7YlF.UgpSLz09ouBeTgUVO', 'user5@gmail.com'),
(333333, 3, '$2a$11$biZMjMmKEd1AA1MRlXQaSu50vWXKvFUCkMfXKC1lTlo8KYRm61EQe', 'janos61@gmail.com'),
(444444, 3, '$2a$11$Ug/pQzldfRX3AvG2h74LourHb107TeLX3HZtuFCIOWVKrA1GngPF6', 'bazsika98@gmail.com'),
(567899, 3, '$2a$11$VkZB0UalfA1fTa0D/ADVxuyz38Sea8Zw17CY2p8wvq/hmQsWQ.5ey', 'user13@gmail.com'),
(662346, 3, '$2a$11$bYBeFv2YXtD0jWgU6tjt0eIHA/Oue8cLi49eeTz.PuZefESps2t8O', 'user6@gmail.com'),
(774234, 3, '$2a$11$Q/Sad4Wwz.T2h8Lb/83aSu0krjTFDH4Xf3KsOiobnPFkRkMlx0uCi', 'user7@gmail.com'),
(823988, 3, '$2a$11$A1.f44yfTOMdUvPQDxB7VOfwBHUaAMLWrIlLiztcuKdrt4C0qRKGS', 'user8@gmail.com'),
(931299, 3, '$2a$11$o895CYxYz.lkKO2ot9PqruAzld0OIjSEwa4otDZw1YCSCe6ZaisIW', 'user9@gmail.com'),
(947654, 3, '$2a$11$Z4bHYb4cbNRP/GXsgbNjRumqqeipdjSG1gxAMWQoveIUb0cH70lbK', 'user11@gmail.com'),
(994239, 3, '$2a$11$I9WsjCUdwnSBuzqxzgm/BOVuVGoZhU3cucZ4kJSv5b.ROm8khPaxO', 'user10@gmail.com');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `fogasok`
--

CREATE TABLE `fogasok` (
  `fogasID` int(11) NOT NULL,
  `azonosito` int(11) NOT NULL,
  `helyszinID` int(11) NOT NULL,
  `horgaszhely` varchar(10) DEFAULT NULL,
  `datum_idopont` datetime NOT NULL,
  `halfaj` varchar(15) DEFAULT NULL,
  `suly` float DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- A tábla adatainak kiíratása `fogasok`
--

INSERT INTO `fogasok` (`fogasID`, `azonosito`, `helyszinID`, `horgaszhely`, `datum_idopont`, `halfaj`, `suly`) VALUES
(1, 111111, 1, NULL, '2021-11-26 14:30:00', 'sügér', 3.5),
(2, 111111, 1, NULL, '2021-11-26 14:45:00', 'széles kárász', 3),
(3, 111111, 1, NULL, '2021-11-26 15:11:00', 'sügér', 1.5),
(4, 111111, 1, NULL, '2021-11-26 16:30:00', 'ponty', 4),
(5, 111111, 2, NULL, '2021-11-30 09:30:00', 'amug', 3.5),
(6, 111111, 2, NULL, '2021-11-30 09:45:00', 'harcsa', 2.5),
(7, 662346, 8, NULL, '2021-10-21 08:30:00', 'ponty', 2),
(8, 662346, 8, NULL, '2021-10-26 08:45:00', 'széles kárász', 3.5),
(9, 662346, 9, NULL, '2021-11-11 15:11:00', 'ponty', 5),
(10, 662346, 9, NULL, '2021-11-11 16:30:00', 'ponty', 3),
(11, 662346, 9, NULL, '2021-11-11 17:30:00', 'márna', 1.5),
(12, 662346, 12, NULL, '2021-11-18 12:45:00', 'márna', 2.5),
(13, 774234, 1, NULL, '2021-09-21 08:30:00', 'harcsa', 2),
(14, 774234, 1, NULL, '2021-09-21 08:45:00', 'harcsa', 1.5),
(15, 774234, 2, NULL, '2021-12-11 07:11:00', 'csuka', 4),
(16, 774234, 2, NULL, '2021-12-11 07:30:00', 'ponty', 3),
(17, 774234, 8, NULL, '2021-12-11 13:30:00', 'amur', 1.5),
(18, 774234, 8, NULL, '2021-12-11 14:45:00', 'amur', 1.5),
(19, 312955, 1, NULL, '2021-02-06 08:30:00', 'csuka', 2),
(20, 312955, 1, NULL, '2021-02-06 08:45:00', 'ponty', 2.5),
(21, 312955, 2, NULL, '2021-05-10 07:11:00', 'csuka', 3),
(22, 312955, 2, NULL, '2021-05-10 09:30:00', 'ponty', 2),
(23, 312955, 9, NULL, '2021-08-25 13:30:00', 'amur', 1),
(24, 312955, 9, NULL, '2021-08-25 14:45:00', 'amur', 4),
(25, 444444, 12, NULL, '2021-02-03 08:30:00', 'csuka', 2),
(26, 444444, 12, NULL, '2021-02-03 10:45:00', 'ponty', 1.5),
(27, 444444, 11, NULL, '2021-05-10 12:11:00', 'csuka', 3),
(28, 444444, 8, NULL, '2021-07-15 13:32:00', 'ponty', 1.5),
(29, 444444, 9, NULL, '2021-08-11 13:30:00', 'amur', 2),
(30, 444444, 12, NULL, '2021-09-21 14:45:00', 'amur', 3),
(31, 222222, 2, NULL, '2021-03-02 08:30:00', 'harcsa', 3),
(32, 222222, 2, NULL, '2021-03-02 10:45:00', 'ponty', 2),
(33, 222222, 2, NULL, '2021-03-02 12:11:00', 'csuka', 2),
(34, 222222, 8, NULL, '2021-07-15 13:30:00', 'ponty', 4),
(35, 222222, 8, NULL, '2021-07-15 13:35:00', 'csuka', 3),
(36, 222222, 9, NULL, '2021-09-21 14:45:00', 'csuka', 2),
(37, 111111, 1, '3', '2021-10-26 14:30:00', 'sügér', 3),
(38, 111111, 1, '3', '2021-10-26 14:45:00', 'széles kárász', 2.5),
(39, 111111, 2, '3', '2021-10-26 15:11:00', 'sügér', 2),
(40, 111111, 2, '3', '2021-10-26 16:30:00', 'ponty', 1),
(41, 111111, 1, '4', '2021-08-30 12:30:00', 'amug', 1),
(42, 111111, 1, '4', '2021-05-23 12:45:00', 'harcsa', 1.5),
(43, 333333, 2, '3', '2021-05-26 12:30:00', 'sügér', 2.5),
(44, 333333, 2, '3', '2021-05-26 12:45:00', 'széles kárász', 4),
(45, 333333, 1, '6', '2021-08-26 13:11:00', 'sügér', 3),
(46, 333333, 1, '6', '2021-08-26 16:30:00', 'ponty', 3),
(47, 333333, 3, '4', '2021-09-30 14:30:00', 'sügér', 2),
(48, 333333, 3, '4', '2021-09-23 14:45:00', 'ponty', 2),
(49, 444444, 2, '2', '2021-06-03 09:30:00', 'csuka', 2),
(50, 444444, 1, '2', '2021-06-03 11:45:00', 'ponty', 3),
(51, 444444, 3, '4', '2021-06-10 12:11:00', 'csuka', 1),
(52, 444444, 2, '4', '2021-06-15 12:32:00', 'ponty', 1.5),
(53, 444444, 2, '4', '2021-06-15 12:35:00', 'harcsa', 2),
(54, 444444, 6, '4', '2021-06-21 13:45:00', 'amur', 3),
(55, 222222, 1, '3', '2021-05-04 08:35:00', 'harcsa', 4),
(56, 222222, 1, '3', '2021-05-04 09:47:00', 'ponty', 2),
(57, 222222, 2, '3', '2021-05-04 11:18:00', 'csuka', 4),
(58, 222222, 2, '1', '2021-06-15 14:33:00', 'ponty', 3),
(59, 222222, 3, '1', '2021-06-15 14:39:00', 'márna', 2),
(60, 222222, 5, '4', '2021-08-21 15:43:00', 'csuka', 1),
(61, 662346, 3, '4', '2021-06-21 08:30:00', 'ponty', 2),
(62, 662346, 2, '4', '2021-07-26 08:45:00', 'széles kárász', 2.5),
(63, 662346, 2, '2', '2021-08-11 15:11:00', 'ponty', 3.5),
(64, 662346, 3, '2', '2021-08-11 16:30:00', 'ponty', 3),
(65, 662346, 2, '2', '2021-08-11 17:30:00', 'csuka', 2.5),
(66, 662346, 3, '3', '2021-09-18 12:45:00', 'márna', 2),
(67, 111111, 4, 'Nincs', '2022-03-22 13:11:00', 'sügér', 4.5);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `helyszinek`
--

CREATE TABLE `helyszinek` (
  `helyszinID` int(11) NOT NULL,
  `vizterulet_neve` varchar(100) NOT NULL,
  `vizterkod` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- A tábla adatainak kiíratása `helyszinek`
--

INSERT INTO `helyszinek` (`helyszinID`, `vizterulet_neve`, `vizterkod`) VALUES
(1, 'Kórógyi csatorna', 600111),
(2, 'Bedegkéri halastó', 600214),
(3, 'Tisza folyó Csongrág megyei szakasza', 600311),
(4, 'Maros folyó ország határtól a tiszáig', 600411),
(5, 'Hármas-Körös folyó a torkolattól a Szelevény községkeleti határánál lévő gátőr-házig', 600511),
(6, 'Csongrádi vízlépcső munkatere', 600611),
(7, 'Gyálai Holt-Tisza (0-12+874 fkm. Szelvény)Lúdvári szivattyúteleptől a szérűskerti zsilipig terjedő s', 600711),
(8, 'Körtvélyesi Holt-Tisza', 600811),
(9, 'Akolszögi Holt-Tisza', 600911),
(10, 'Maty-éri víztározó', 603211),
(11, 'Deszki tavak', 603314),
(12, 'Vértó', 607015),
(13, 'Antal-tó', 604014);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `szerepkorok`
--

CREATE TABLE `szerepkorok` (
  `szerepkorID` int(11) NOT NULL,
  `szerepkor_megnev` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- A tábla adatainak kiíratása `szerepkorok`
--

INSERT INTO `szerepkorok` (`szerepkorID`, `szerepkor_megnev`) VALUES
(1, 'adminisztrator'),
(2, 'halor'),
(3, 'felhasznalo');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `felhasznalok`
--
ALTER TABLE `felhasznalok`
  ADD PRIMARY KEY (`azonosito`),
  ADD UNIQUE KEY `email_cim` (`email_cim`),
  ADD KEY `szerepkorID` (`szerepkorID`);

--
-- A tábla indexei `fogasok`
--
ALTER TABLE `fogasok`
  ADD PRIMARY KEY (`fogasID`),
  ADD UNIQUE KEY `datum_idopont` (`datum_idopont`,`azonosito`,`halfaj`,`suly`),
  ADD KEY `helyszinID` (`helyszinID`) USING BTREE,
  ADD KEY `fogasok_ibfk_1` (`azonosito`);

--
-- A tábla indexei `helyszinek`
--
ALTER TABLE `helyszinek`
  ADD PRIMARY KEY (`helyszinID`),
  ADD UNIQUE KEY `vizterulet_neve` (`vizterulet_neve`),
  ADD UNIQUE KEY `vizterkod` (`vizterkod`);

--
-- A tábla indexei `szerepkorok`
--
ALTER TABLE `szerepkorok`
  ADD PRIMARY KEY (`szerepkorID`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `fogasok`
--
ALTER TABLE `fogasok`
  MODIFY `fogasID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=68;

--
-- AUTO_INCREMENT a táblához `helyszinek`
--
ALTER TABLE `helyszinek`
  MODIFY `helyszinID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1000;

--
-- AUTO_INCREMENT a táblához `szerepkorok`
--
ALTER TABLE `szerepkorok`
  MODIFY `szerepkorID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `felhasznalok`
--
ALTER TABLE `felhasznalok`
  ADD CONSTRAINT `felhasznalok_ibfk_1` FOREIGN KEY (`szerepkorID`) REFERENCES `szerepkorok` (`szerepkorID`);

--
-- Megkötések a táblához `fogasok`
--
ALTER TABLE `fogasok`
  ADD CONSTRAINT `fogasok_ibfk_1` FOREIGN KEY (`azonosito`) REFERENCES `felhasznalok` (`azonosito`),
  ADD CONSTRAINT `fogasok_ibfk_2` FOREIGN KEY (`helyszinID`) REFERENCES `helyszinek` (`helyszinID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
