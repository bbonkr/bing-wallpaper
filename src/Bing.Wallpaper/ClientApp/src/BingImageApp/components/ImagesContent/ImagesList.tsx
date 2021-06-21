import React from 'react';
import { ImageItemModel } from '../../models';
import { ImageCard } from './ImageCard';

interface ImagesListProps {
    images: ImageItemModel[];
}

export const ImagesList = ({ images }: ImagesListProps) => {
    return (
        <div className="columns is-multiline is-justify-content-center is-align-items-center">
            {images.map((image) => (
                <ImageCard key={image.id} image={image} />
            ))}
        </div>
    );
};
