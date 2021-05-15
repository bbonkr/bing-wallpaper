import { useSelector, useDispatch } from 'react-redux';
import { RootState } from '../../store/reducers';
import { ImagesState } from '../../store/reducers/images';
import imagesActions from '../../store/actions/images';
import { ImageItemModel, LoadImagesRequestModel } from '../../models';

export const useImagesApi = () => {
    const dispatch = useDispatch();
    const images = useSelector<RootState, ImagesState>((state) => state.images);

    return {
        ...images,
        loadImagesRequest: (payload: LoadImagesRequestModel) =>
            dispatch(imagesActions.loadImages.request(payload)),
        appendImagesRequest: (payload: LoadImagesRequestModel) =>
            dispatch(imagesActions.appendImages.request(payload)),
        resetImagesError: () => dispatch(imagesActions.resetLoadImagesError()),
        showFullSizeImage: (payload: ImageItemModel) =>
            dispatch(imagesActions.showFullSizeImage(payload)),
        hideFullSizeImage: () => dispatch(imagesActions.hideFullSizeImage()),
    };
};

export type UseImagesApi = ReturnType<typeof useImagesApi>;
