import { ImagesApiClient } from '../lib/clients/ImageApiClient';

const services = {
    images: new ImagesApiClient(),
};

export default services;
export type Services = typeof services;
