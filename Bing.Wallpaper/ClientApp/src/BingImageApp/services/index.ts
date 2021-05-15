import { ImagesApiClient } from '../lib/clients/ImageApiClient';
import { LogsApiClient } from '../lib/clients/LogApiClient';

const services = {
    images: new ImagesApiClient(),
    logs: new LogsApiClient(),
};

export default services;
export type Services = typeof services;
