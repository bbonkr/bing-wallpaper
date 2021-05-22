import { ApiClientBase } from '../ApiClientBase';
import {
    ImagesApiResponseModel,
    LoadImagesRequestModel,
} from '../../../models';

export class ImagesApiClient extends ApiClientBase {
    public async getImages(
        options: LoadImagesRequestModel,
    ): Promise<ImagesApiResponseModel> {
        const url = `${this.getBaseUrl()}?page=${options.page}&take=${
            options.take
        }`;

        const response = await this.getClient().get<ImagesApiResponseModel>(
            url,
        );

        return this.returnsResponseModelIfSucceed(response);
    }

    protected getBaseUrl(): string {
        return `${super.getBaseUrl()}/images`;
    }
}
