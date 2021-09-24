import axios, { AxiosError, AxiosInstance, AxiosResponse } from 'axios';
import {
    ObjectApiResponseModel,
    ImagesApi,
    LogsApi,
    BingImagesApi,
} from '../../api/api';
import { Configuration } from '../../api/configuration';

export class ApiClient {
    constructor() {
        const configuration: Configuration = new Configuration({});
        const axiosInstance = this.getAxiosInstance();
        axiosInstance.interceptors.response.use(
            (res) => res,
            (err) => {
                if (axios.isAxiosError(err)) {
                    const axiosErr = err as AxiosError<ObjectApiResponseModel>;

                    throw axiosErr.response;
                }
                throw err;
            },
        );

        this.logs = new LogsApi(configuration, '', axiosInstance);
        this.images = new ImagesApi(configuration, '', axiosInstance);
        this.bingImage = new BingImagesApi(configuration, '', axiosInstance);
    }

    public readonly images: ImagesApi;
    public readonly bingImage: BingImagesApi;
    public readonly logs: LogsApi;

    private getAxiosInstance(): AxiosInstance {
        const instance = axios.create();

        return instance;
    }
}

export type ErrorResponse = AxiosResponse<ObjectApiResponseModel>;
