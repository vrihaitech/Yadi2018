/*----version 1.0.1.2*/
USE [Yadi2018]
--altered on 01/30/2019--Jugal
ALTER TABLE dbo.TStock ADD
	ContainerCharges numeric(18, 2) NULL,
	ContainerChargesAmt numeric(18, 2) NULL,
	PackagingChargesAmt numeric(18, 2) NULL
