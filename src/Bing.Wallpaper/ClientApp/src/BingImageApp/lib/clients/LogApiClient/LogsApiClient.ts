import { LogsApiResponseModel, LoadLogsRequestModel } from '../../../models';
import { ApiClientBase } from '../ApiClientBase';

export class LogsApiClient extends ApiClientBase {
    public async getLogs(
        options: LoadLogsRequestModel,
    ): Promise<LogsApiResponseModel> {
        const url = `${this.getBaseUrl()}?page=${options.page}&take=${
            options.take
        }&level=${options.level}&keyword=${encodeURIComponent(
            options.keyword ?? '',
        )}`;

        const response = await this.getClient().get<LogsApiResponseModel>(url);

        return this.returnsResponseModelIfSucceed(response);
    }

    protected getBaseUrl(): string {
        return `${super.getBaseUrl()}/logs`;
    }
}
