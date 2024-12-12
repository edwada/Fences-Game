package com.example.fences_android.core

class Event<T> {
    val subscribers = ArrayList<(T) -> Unit>()

    fun Subscribe(observer: (T) -> Unit) {
        subscribers.add(observer);
    }

    fun Raise(param: T) {
        for (s in subscribers){
            s(param);
        }
    }

    fun Remove(observer: (T) -> Unit){
        subscribers.remove(observer);
    }
}