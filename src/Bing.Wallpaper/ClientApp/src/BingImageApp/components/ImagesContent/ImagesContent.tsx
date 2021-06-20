import React, { useEffect, useState } from 'react';
import { ImagesList } from './ImagesList';
import { useImagesApi } from '../../hooks/useImagesApi';
import { Content, Section } from '../Layouts';
import { FaSync } from 'react-icons/fa';

export const ImagesContent = () => {
    const [page, setPage] = useState(1);
    const take = 10;

    const {
        images,
        isLoadingImages,
        hasMoreImages,
        loadImagesRequest,
        appendImagesRequest,
    } = useImagesApi();

    const handleClickLoadMore = () => {
        if (hasMoreImages) {
            appendImagesRequest({ page: page + 1, take: take });
            setPage((state) => state + 1);
        }
    };

    const handleClickRefresh = () => {
        loadImagesRequest({ page: 1, take: take });
        setPage((_) => 1);
    };

    useEffect(() => {
        if ((images ?? []).length === 0) {
            loadImagesRequest({ page: page, take: take });
            setPage((_) => 1);
        }
    }, []);

    return (
        <Content classNames={[]}>
            <Section
                title={
                    <React.Fragment>
                        Bing Today Images{' '}
                        <button
                            className={`button ${
                                isLoadingImages ? 'is-loading' : ''
                            }`}
                            disabled={isLoadingImages}
                            onClick={handleClickRefresh}
                            title="Reload images"
                        >
                            <FaSync />
                        </button>
                    </React.Fragment>
                }
                useHero
                heroColor="is-primary"
                heroSize="is-small"
            />

            <Section classNames={['p-6']}>
                <ImagesList images={images ?? []} />
                {hasMoreImages && (
                    <div className="is-flex is-flex-direction-column is-justify-content-center">
                        <button
                            className={`button ${
                                isLoadingImages ? 'is-loading' : ''
                            }`}
                            disabled={isLoadingImages}
                            onClick={handleClickLoadMore}
                            title="Load more images"
                        >
                            Load more
                        </button>
                    </div>
                )}
            </Section>
        </Content>
    );
};
