import React from 'react';
import { ImageItemModel } from '../../../api/api';
import { UiHelper } from '../../lib/UiHelper';
import { ImageCard } from './ImageCard';

interface ImagesListProps {
    images: ImageItemModel[];
}

export const ImagesList = ({ images }: ImagesListProps) => {
    return (
        <React.Fragment>
            {images.map((image) => (
                <ImageCard key={image.id} image={image} />
            ))}
        </React.Fragment>
    );
};

interface ListContainerProps {
    isHorizontalAlignmentCenter?: boolean;
}

export const ListContainer = ({
    isHorizontalAlignmentCenter,
    children,
}: React.PropsWithChildren<ListContainerProps>) => {
    return (
        <div
            className={UiHelper.getClassNames(
                ...[
                    'columns',
                    'is-multiline',
                    'is-align-items-center',
                    isHorizontalAlignmentCenter
                        ? 'is-justify-content-center'
                        : '',
                ].filter(Boolean),
            )}
        >
            {children}
        </div>
    );
};
