import React from 'react';
import { useBingImagesApi } from '../../hooks/useBingImagesApi/useBingImagesApi';
import { Content, Section } from '../Layouts';
import Loading from '../Loading';
import { CollectorForm } from './CollectorForm';

export const ImageCollector = () => {
    const {
        collectRequest,
        isLoadingCollectImages,
        resetCollectErrorRequest,
        collectImagesError,
        collectResult,
    } = useBingImagesApi();

    const handleCollect = (startIndex: number, take: number) => {
        collectRequest({ startIndex: startIndex, take: take });
    };

    return (
        <Content classNames={[]}>
            <Section
                title="Collector"
                useHero
                heroSize="is-small"
                heroColor="is-warning"
            />
            <Section>
                <CollectorForm
                    isLoading={isLoadingCollectImages}
                    onCollect={handleCollect}
                />
            </Section>
            <Section>
                {isLoadingCollectImages ? (
                    <Loading message="Collecting ..." />
                ) : collectImagesError ? (
                    <div className="is-flex-grow-1 is-flex is-flex-direction-column is-justify-content-center is-align-items-center">
                        {collectImagesError?.message}
                    </div>
                ) : (
                    <div className="is-flex-grow-1 is-flex is-flex-direction-column is-justify-content-center is-align-items-center">
                        {collectResult?.message}
                    </div>
                )}
            </Section>
        </Content>
    );
};

export default ImageCollector;
