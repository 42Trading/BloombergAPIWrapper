﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bloomberglp.Blpapi;
using System.Reflection;
using BloombergAPIWrapper.Data.Attributes;
using ExtendedDataCollections.Collections;

namespace BloombergAPIWrapper.Messaging.Responses
{
    /// <summary>
    /// Bloomberg intraday data response type, processes messages with description content, cracked and parsed result is accessible direcly in class through indexer or properties.
    /// </summary>
    /// <typeparam name="T">Custom type with defined attributes that determine how is message parsed.</typeparam>
    public class BloombergIntradayTickDataResponse<T> : BloombergResponse<T>
    {
        Type t = typeof(T);
        BigList<T> data = new BigList<T>();

        public BloombergIntradayTickDataResponse(object RequestClass) : base(RequestClass) { }

        /// <summary>
        /// Response data in form of list.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public BigList<T> Data { get { return data; } }

        public T this[ulong index]
        {
            get { return data[index]; }
        }

        /// <summary>
        /// Processes the Intraday data messages.
        /// </summary>
        internal override void ProcessMessage(Message message)
        {            
                Element tickListData = message.GetElement("tickData");
                CheckErrors(tickListData);
                Element tickData = tickListData.GetElement("tickData");
                CheckErrors(tickData);
                for (int j = 0; j < tickData.NumValues; j++)
                    {
                        Element el = (Element)tickData[j];
                        CheckErrors(el);
                        data.Add(ProcessFields(el));
                    }
                    
            
        
        }

       
    }
}
