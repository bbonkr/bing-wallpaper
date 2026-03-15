import { useDispatch, useSelector } from 'react-redux';
import { RootState } from '../../store/reducers';
import { BingImagesState } from '../../store/reducers/bingImages';
import { bingImagesActions } from '../../store/actions/bingImages';
import { BingImageServiceGetRequestModel } from '../../../api/api';

export const useBingImagesApi = () => {
    const dispatch = useDispatch();
    const state = useSelector<RootState, BingImagesState>((s) => s.bingImages);
    return {
        ...state,
        collectRequest: (payload: BingImageServiceGetRequestModel) =>
            dispatch(bingImagesActions.collectImages.request(payload)),
        resetCollectErrorRequest: () =>
            dispatch(bingImagesActions.resetCollectImagesError()),
    };
};
