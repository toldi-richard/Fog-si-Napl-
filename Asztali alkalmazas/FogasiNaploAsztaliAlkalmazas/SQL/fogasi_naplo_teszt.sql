-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2022. Már 18. 13:38
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
-- Adatbázis: `fogasi_naplo_teszt`
--
CREATE DATABASE IF NOT EXISTS `fogasi_naplo_teszt` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `fogasi_naplo_teszt`;

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
(1, 111111, 1, NULL, '2022-03-18 10:13:58', 'Amur', 4),
(2, 111111, 2, NULL, '2022-03-09 10:13:58', 'Csuka', 5),
(7, 111111, 2, NULL, '2022-03-09 10:13:58', 'Csuka', 4),
(8, 111111, 2, NULL, '2022-03-09 10:13:58', 'Teszt', 3),
(9, 111111, 1, NULL, '2022-03-09 10:13:58', 'Teszt', 6),
(10, 111111, 1, NULL, '2022-03-09 10:13:58', 'Teszt', 10);

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
  MODIFY `fogasID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

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
-- Megkötések a táblához `fogasok`
--
ALTER TABLE `fogasok`
  ADD CONSTRAINT `fogasok_ibfk_1` FOREIGN KEY (`azonosito`) REFERENCES `felhasznalok` (`azonosito`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
