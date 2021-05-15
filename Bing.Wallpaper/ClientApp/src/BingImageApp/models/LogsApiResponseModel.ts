import { ApiResponseModel } from './ApiResponseModel';

export interface LogModel {
    id: string;
    machineName: string;
    logged: string;
    level: string;
    message: string;
    logger: string;
    callsite: string;
    exception: string;
}

export interface LogsApiResponseModel extends ApiResponseModel<LogModel[]> {}

export interface LoadLogsRequestModel {
    page: number;
    take: number;
    level: string;
    keyword: string;
}
