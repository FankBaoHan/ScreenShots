﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ScreenShoot
{
    public class RecognitionHelper
    {
        private const string API_KEY = "*";
        private const string SECRET_KEY = "*";

        public static StringBuilder recognize(Bitmap bitmap)
        {
            var image = BitmapHelper.bitmap2Byte(bitmap);

            StringBuilder sb = new StringBuilder();

            var client = new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);
            client.Timeout = 6000;

            var result = client.AccurateBasic(image);

            var options = new Dictionary<string, object>{
                {"language_type", "ENG"},//语言
                {"detect_direction", "true"},//图片方向
                {"probability", "true"}//图片识别成功可能性
             };

            result = client.AccurateBasic(image, options);

            string getJson = result.ToString();
            JsonImage.Root rt = JsonConvert.DeserializeObject<JsonImage.Root>(getJson);//JSON反序列化

            for (int i = 0; i < rt.words_result.Count; i++)
            {
                sb.AppendLine(rt.words_result[i].words).ToString();
            }

            return sb;
        }
    }
}
