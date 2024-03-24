export class Product {
    productId!: number;
    price!: number;
    image!: string;
    brandId!: number;
    productTypeId!: number;
    productType: any;
    brand: any;
    name!: string;
    description!: string;
    dateCreated!: string;
    dateModified!: string | null;
    isActive!: boolean;
    isDeleted!: boolean;
  }