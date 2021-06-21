import axios, { AxiosInstance, AxiosResponse } from 'axios';

export abstract class ApiClientBase {
    protected getClient(): AxiosInstance {
        const instance = axios.create();

        return instance;
    }

    protected getBaseUrl(): string {
        return '/api/v1.0';
    }

    protected returnsResponseModelIfSucceed<T>(response: AxiosResponse<T>): T {
        if (this.isSuccess(response.status)) {
            return response.data;
        } else {
            throw response.data;
        }
    }

    private isSuccess(statusCode: number): boolean {
        return statusCode >= 200 && statusCode < 300;
    }
}
