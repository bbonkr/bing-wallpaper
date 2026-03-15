import { ApiResponseModel } from './ApiResponseModel';
import { PagedModel } from './PagedModel';
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

export interface LogsApiResponseModel
    extends ApiResponseModel<PagedModel<LogModel>> {}

export interface LoadLogsRequestModel {
    page: number;
    take: number;
    level: string;
    keyword: string;
}
