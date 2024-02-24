drop table products
CREATE TABLE products
(
    id INT PRIMARY KEY IDENTITY(1,1),
    prod_id VARCHAR(MAX) NULL,
    prod_name VARCHAR(MAX) NULL,
    prod_type VARCHAR(MAX) NULL,
    prod_stock INT NULL,
    prod_price FLOAT NULL,
    prod_status VARCHAR(MAX) NULL,
    prod_image VARCHAR(MAX) NULL,
    date_insert DATE NULL,
    date_update DATE NULL,
    date_delete DATE NULL
);




CREATE TABLE customers
(
    id INT PRIMARY KEY IDENTITY(1,1),
    customer_id INT NULL,
    total_price FLOAT NULL,
    date DATE NULL
);


CREATE TABLE orders
(
    id INT PRIMARY KEY IDENTITY(1,1),
    customer_id INT NULL,
    prod_id VARCHAR(MAX) NULL,
    prod_name VARCHAR(MAX) NULL,
    prod_type VARCHAR(MAX) NULL,
    prod_price FLOAT NULL,
    order_date DATE NULL,
    delete_order DATE NULL,
    qty INT NULL
);

SELECT * FROM orders

ALTER TABLE orders
ADD qty INT NULL

INSERT INTO customers (customer_id, total_price, date)
VALUES
    (1001, 50.25, '2024-01-15'),
    (1002, 75.50, '2024-01-16'),
    (1003, 120.00, '2024-01-17'),
    (1004, 90.75, '2024-01-18'),
    (1005, 60.90, '2024-01-19');


        drop table customers



-- Insert sample dummy data
INSERT INTO customers (customer_id, total_price, amount, change, date)
VALUES
    (1001, 50.25, 60.00, 9.75, '2024-01-15'),
    (1002, 75.50, 80.00, 4.50, '2024-01-16'),
    (1003, 120.00, 130.00, 10.00, '2024-01-17'),
    (1004, 90.75, 100.00, 9.25, '2024-01-18'),
    (1005, 60.90, 70.00, 9.10, '2024-01-19');


SELECT * FROM customers
SELECT * FROM orders
SELECT * FROM users

    drop table orders

CREATE TABLE orders
(
    id INT PRIMARY KEY IDENTITY(1,1),
    customer_id INT NULL,
    prod_id VARCHAR(MAX) NULL,
    prod_name VARCHAR(MAX) NULL,
    prod_type VARCHAR(MAX) NULL,
    prod_price FLOAT NULL,
    order_date DATE NULL,
    delete_order DATE NULL,
    qty INT NULL
);


    ALTER TABLE orders
    ADD qty INT NULL;