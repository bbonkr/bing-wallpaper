import { combineReducers } from 'redux';
import { createReducer } from 'typesafe-actions';
import { ApiResponseModel, ImageItemModel } from '../../models';
import imagesActions, { ImagesActionTypes } from '../actions/images';

export const isLoadingImages = createReducer<boolean, ImagesActionTypes>(false)
    .handleAction([imagesActions.loadImages.request], (state, action) => true)
    .handleAction(
        [imagesActions.loadImages.success, imagesActions.loadImages.failure],
        (state, action) => false,
    );

export const loadImagesError = createReducer<
    ApiResponseModel | null,
    ImagesActionTypes
>(null)
    .handleAction(
        [
            imagesActions.loadImages.request,
            imagesActions.loadImages.success,
            imagesActions.resetLoadImagesError,
        ],
        (state, action) => null,
    )
    .handleAction(
        [imagesActions.loadImages.failure],
        (state, action) => action.payload,
    );

export const images = createReducer<
    ImageItemModel[] | undefined,
    ImagesActionTypes
>([]).handleAction([imagesActions.loadImages.success], (state, action) => {
    return [...(state ?? []), ...(action.payload.data ?? [])];
});

export const hasMoreImages = createReducer<boolean, ImagesActionTypes>(true)
    .handleAction(
        [imagesActions.loadImages.success],
        (state, action) => (action.payload.data ?? []).length > 0,
    )
    .handleAction(
        [imagesActions.loadImages.request, imagesActions.loadImages.failure],
        (state, action) => true,
    );

export const fullSizeImage = createReducer<
    ImageItemModel | null,
    ImagesActionTypes
>(null)
    .handleAction(
        [imagesActions.showFullSizeImage],
        (state, action) => action.payload,
    )
    .handleAction([imagesActions.hideFullSizeImage], (state, action) => null);

const imagesState = combineReducers({
    isLoadingImages,
    images,
    hasMoreImages: hasMoreImages,
    fullSizeImage,
});

export default imagesState;
export type ImagesState = ReturnType<typeof imagesState>;
