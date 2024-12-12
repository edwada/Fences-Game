package com.example.fences_android;

import static org.junit.Assert.fail;

import java.lang.reflect.Type;

abstract class TestHelper {
     public static Exception AssertThrows(Type t, Runnable r) {
        boolean success = false;
        Exception result = null;
        try {
            r.run();
        } catch (Throwable ex) {

            if (ex.getClass() == t){
                success = true;
                result = (Exception) ex;
            }
        }

        if (success) {
            return result;
        }

        fail();
        return null;
    }
}
