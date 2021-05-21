import axios from 'axios';
import {
    ImagesApiResponseModel,
    LoadImagesRequestModel,
} from '../../../models';

export class ImagesApiClient {
    public async getImages(
        options: LoadImagesRequestModel,
    ): Promise<ImagesApiResponseModel> {
        const url = `/api/v1.0/images?page=${options.page}&take=${options.take}`;

        const response = await axios.get<ImagesApiResponseModel>(url);
        if (response.status === 200) {
            return response.data;
        } else {
            throw response;
        }
    }
}
