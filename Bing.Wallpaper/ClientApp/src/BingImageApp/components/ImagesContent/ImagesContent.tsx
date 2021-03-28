import React, { useEffect, useState } from 'react';
import { ImagesList } from './ImagesList';
import { useImagesApi } from '../../hooks/useImagesApi';

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
        <div className="container-fluid mb-20 pb-20">
            <div className="content">
                <ImagesList images={images ?? []} />
            </div>
            {hadMoreImages && (
                <div className="d-flex flex-column justify-content-center align-items-center">
                    <button
                        className="btn"
                        disabled={isLoadingImages}
                        onClick={handleClickLoadMore}
                    >
                        {isLoadingImages ? 'Loading ...' : 'Load more images'}
                    </button>
                </div>
            )}
        </div>
    );
};
