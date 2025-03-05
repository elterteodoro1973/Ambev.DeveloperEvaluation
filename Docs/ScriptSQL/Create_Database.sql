-- Database: Ambev
-- DROP DATABASE IF EXISTS "Ambev";

CREATE DATABASE "Ambev"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'pt-BR'
    LC_CTYPE = 'pt-BR'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;
	
CREATE SCHEMA IF NOT EXISTS "DeveloperEvaluation"   AUTHORIZATION postgres;	

CREATE TABLE IF NOT EXISTS "DeveloperEvaluation"."User"
(
    "Id" uuid NOT NULL DEFAULT gen_random_uuid(),
    "Name" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    "Role" character varying(100) COLLATE pg_catalog."default",
    "Email" character varying(256) COLLATE pg_catalog."default" NOT NULL,
    "Phone" character varying(20) COLLATE pg_catalog."default",
    "Password" character varying(512) COLLATE pg_catalog."default" NOT NULL,
    "Status" character varying(20) COLLATE pg_catalog."default",
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS "DeveloperEvaluation"."User"  OWNER to postgres;


CREATE TABLE IF NOT EXISTS "DeveloperEvaluation"."Customer"
(
    "Id" uuid NOT NULL DEFAULT gen_random_uuid(),
    "Name" character(100) COLLATE pg_catalog."default",
    "Phone" character(20) COLLATE pg_catalog."default" NOT NULL,
    "Email" character(256) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Customer_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS "DeveloperEvaluation"."Customer"  OWNER to postgres;

COMMENT ON TABLE "DeveloperEvaluation"."Customer"  IS 'Cliente';


CREATE TABLE IF NOT EXISTS "DeveloperEvaluation"."Product"
(
    "Id" uuid NOT NULL DEFAULT gen_random_uuid(),
    "Title" character varying(100) COLLATE pg_catalog."default",
    "Price" numeric(10,2) DEFAULT 0,
    "QuantityInStock" integer DEFAULT 0,
    "Description" character varying(100) COLLATE pg_catalog."default",
    "Category" character varying(100) COLLATE pg_catalog."default",
    "Image" character varying(256) COLLATE pg_catalog."default",
    "Code" character varying(10) COLLATE pg_catalog."default",
    CONSTRAINT "PK_Product" PRIMARY KEY ("Id"),
    CONSTRAINT "UQ_Product_Code" UNIQUE ("Code")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS "DeveloperEvaluation"."Product"  OWNER to postgres;

CREATE TABLE IF NOT EXISTS "DeveloperEvaluation"."Sale"
(
    "Id" uuid NOT NULL DEFAULT gen_random_uuid(),
    "CustomerId" uuid NOT NULL,
    "TotalGrossValue" numeric(12,2),
    "Discounts" numeric(4,2),
    "TotalNetValue" numeric(12,2),
    "Cancelled" boolean,
    "SaleDate" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Sale" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Sale_Customer_Id" FOREIGN KEY ("CustomerId")
        REFERENCES "DeveloperEvaluation"."Customer" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS "DeveloperEvaluation"."Sale" OWNER to postgres;


CREATE TABLE IF NOT EXISTS "DeveloperEvaluation"."SaleItems"
(
    "SaleId" uuid NOT NULL,
    "Quantities" integer NOT NULL DEFAULT 1,
    "UnitPrices" numeric(10,2) NOT NULL,
    "CodeProduct" character varying(10) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "PK_SaleItems" PRIMARY KEY ("SaleId", "CodeProduct"),
    CONSTRAINT "FK_SaleItems_ProductCode" FOREIGN KEY ("CodeProduct")
        REFERENCES "DeveloperEvaluation"."Product" ("Code") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID,
    CONSTRAINT "FK_SaleItems_SaleId" FOREIGN KEY ("SaleId")
        REFERENCES "DeveloperEvaluation"."Sale" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS "DeveloperEvaluation"."SaleItems"  OWNER to postgres;

COMMENT ON CONSTRAINT "PK_SaleItems" ON "DeveloperEvaluation"."SaleItems"  IS 'CodeProduct';

--Criação de usuario inicial: Name=> Admin  Senha:123654@Es"
INSERT INTO "DeveloperEvaluation"."User"("Id", "Name", "Role", "Email", "Phone", "Password", "Status", "CreatedAt", "UpdatedAt")
VALUES (gen_random_uuid(), 'Admin', 'Admin', 'admin@gmail.com','3130309692' ,  '$2a$11$JBvD.raYMpkJ/4AmPQH3gOLhpn1/aD5p4X2MNtUxZ6ujy3d77M9me', 'Active', now(),  null);	

--Criação de cliente inicial
INSERT INTO "DeveloperEvaluation"."Customer"("Id", "Name", "Phone", "Email")
	VALUES (gen_random_uuid(), 'Marcelo Lima Xavier', '31971001001', 'marcelolima99@gmail.com');

--Criação de 2 produtos iniciais
INSERT INTO "DeveloperEvaluation"."Product"( "Id","Code","Price","QuantityInStock","Description","Category","Image")
	VALUES (gen_random_uuid(), 'A00100', 352.17, 10 , 'Mesa 4 cadeiras - Willian' , 'Movel', 'mesa4Cadeira.jpg');


INSERT INTO "DeveloperEvaluation"."Product"("Id","Code","Price","QuantityInStock","Description","Category","Image")
	VALUES (gen_random_uuid(), 'A00101', 158.09, 10 , 'Panela de pressão Elétrica, PCC20, 6L', 'Utensílio domestico', 'mesa4Cadeira.jpg');	

				   
