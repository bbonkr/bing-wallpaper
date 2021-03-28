import React from 'react';
import { ImageItemModel } from '../../models';
import { ImageCard } from './ImageCard';

interface ImagesListProps {
    images: ImageItemModel[];
}

export const ImagesList = ({ images }: ImagesListProps) => {
    return (
        <div className="d-flex flex-row flex-wrap justify-content-center">
            {images.map((image) => (
                <ImageCard key={image.id} image={image} />
            ))}
        </div>
    );
};
