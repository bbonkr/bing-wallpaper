import axios, { AxiosError, AxiosInstance, AxiosResponse } from 'axios';
import { ApiResponseModel, ImagesApi, LogsApi } from '../../api/api';
import { Configuration } from '../../api/configuration';

export class ApiClient {
    constructor() {
        const configuration: Configuration = new Configuration({});
        const axiosInstance = this.getAxiosInstance();
        axiosInstance.interceptors.response.use(
            (res) => res,
            (err) => {
                if (axios.isAxiosError(err)) {
                    const axiosErr = err as AxiosError<ApiResponseModel>;

                    throw axiosErr.response;
                }
                throw err;
            },
        );

        this.logs = new LogsApi(configuration, '', axiosInstance);
        this.images = new ImagesApi(configuration, '', axiosInstance);
    }

    public readonly images: ImagesApi;
    public readonly logs: LogsApi;

    private getAxiosInstance(): AxiosInstance {
        const instance = axios.create();

        return instance;
    }
}

export type ErrorResponse = AxiosResponse<ApiResponseModel>;
