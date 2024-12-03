export type User = {
    username: string,
    token: string
}

export type address = {
    district: string,
    division: string,
    country: string,
    postalCode: string
}

export interface addProduct {
    name: string;
    description: string;
    price: number;
    pictureUrl: string;
    type: string;
    brand: string;
    quantityInStock: number;
  }
  