import axios from 'axios';
import { LogsApiResponseModel, LoadLogsRequestModel } from '../../../models';

export class LogsApiClient {
    public async getLogs(
        options: LoadLogsRequestModel,
    ): Promise<LogsApiResponseModel> {
        var url = `/api/v1.0/logs?page=${options.page}&take=${
            options.take
        }&level=${options.level}&keyword=${encodeURIComponent(
            options.keyword ?? '',
        )}`;

        var response = await axios.get<LogsApiResponseModel>(url);
        if (response.status === 200) {
            return response.data;
        } else {
            throw response;
        }
    }
}
