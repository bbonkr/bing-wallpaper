import React, { useEffect, useState } from 'react';
import { ImagesList } from './ImagesList';
import { useImagesApi } from '../../hooks/useImagesApi';
import { Content } from '../Layouts';

export const ImagesContent = () => {
    const [page, setPage] = useState(1);
    const take = 10;

    const {
        images,
        isLoadingImages,
        hasMoreImages: hadMoreImages,
        loadImagesRequest,
    } = useImagesApi();

    const handleClickLoadMore = () => {
        if (hadMoreImages) {
            loadImagesRequest({ page: page + 1, take: take });
            setPage((state) => state + 1);
        }
    };

    useEffect(() => {
        if ((images ?? []).length === 0) {
            loadImagesRequest({ page: page, take: take });
        }
    }, []);

    return (
        <Content classNames={['p-6']}>
            <ImagesList images={images ?? []} />
            {hadMoreImages && (
                <div className="is-flex is-flex-direction-column is-justify-content-center">
                    <button
                        className="button"
                        disabled={isLoadingImages}
                        onClick={handleClickLoadMore}
                    >
                        {isLoadingImages ? 'Loading ...' : 'Load more images'}
                    </button>
                </div>
            )}
        </Content>
    );
};
