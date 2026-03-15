import { ActionType, createAction, createAsyncAction } from 'typesafe-actions';
import {
    ObjectApiResponseModel,
    BingImageServiceGetRequestModel,
} from '../../../api/api';

export const collectImages = createAsyncAction(
    'collect-images/reuqest',
    'collect-images/success',
    'collect-images/failure',
)<
    BingImageServiceGetRequestModel,
    ObjectApiResponseModel,
    ObjectApiResponseModel
>();

export const resetCollectImagesError = createAction(
    'collect-images/reset-error',
)();

export const bingImagesActions = {
    collectImages,
    resetCollectImagesError,
};

export type BingImagesActions = ActionType<typeof bingImagesActions>;
