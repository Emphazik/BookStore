use BookStoreH
CREATE TABLE Books (
    idBook INT PRIMARY KEY,
	Title VARCHAR(255) NOT NULL,
    ISBN VARCHAR(13) UNIQUE NOT NULL,
    Year_of_publication INT NOT NULL,
    Publisher INT,
    Author INT,
    Category INT,
    Size VARCHAR(50),
    Weight DECIMAL(10, 2),
    Pages INT,
    FOREIGN KEY (Publisher) REFERENCES Publishers(idPublisher),
    FOREIGN KEY (Author) REFERENCES Authors(idAuthor),
    FOREIGN KEY (Category) REFERENCES Categories(idCategory)
);
