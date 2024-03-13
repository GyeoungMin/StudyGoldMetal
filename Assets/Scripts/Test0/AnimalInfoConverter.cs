using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;
public class AnimalInfoConverter : JsonConverter
{
    // 매개변수 objectType의 부모 클래스가 AnimalInfo 인지 IsAssignableFrom()를 통해 확인하여 Convert가 가능한지에 대한 bool값을 return해준다.
    public override bool CanConvert(Type objectType) => typeof(AnimalInfo).IsAssignableFrom(objectType);

    // ReadJson 메서드를 구현하여 JSON 데이터를 역직렬화할 때 특정 조건에 따라 원하는 클래스로 변환하도록 한다.
    // 이를 통하여 JsonConvert.DeserializeObject<T>()의 두번째 매개변수로 해당 메소드를 설정하여 사용할 수 있다.
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jsonObject = JObject.Load(reader);

        // type 값에 따라 적절한 클래스로 변환
        int type = jsonObject["type"].Value<int>();
        if (type == 0)
        {
            return jsonObject.ToObject<HerbivoreInfo>();
        }
        else if (type == 1)
        {
            return jsonObject.ToObject<CarnivoreInfo>();
        }

        return null;
    }

    // WriteJson 메서드는 JSON으로의 직렬화 작업을 담당하지만 일반적으로 이 메서드는 구현되지 않거나, 예외를 throw하도록 만들어진다.
    //public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) { throw new NotImplementedException(); }
    // 직렬화를 지원할 필요가 있다면 구현해야 한다.
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        // value가 AnimalInfo의 객체 인지 확인하고 진행한다.
        if (value is AnimalInfo)
        {
            // value == animalInfo 이므로 as를 통하여 가독성을 높였다.
            AnimalInfo animalInfo = value as AnimalInfo;

            // 쓰기 시작
            writer.WriteStartObject();

            // animalInfo에 존재하는 Properties를 foreach로 각 property에 해당 name과 value들을 넣어주었다.
            foreach (var property in animalInfo.GetType().GetProperties())
            {
                writer.WritePropertyName(property.Name);
                writer.WriteValue(property.GetValue(animalInfo));
            }

            // AnimalInfo의 자식 클래스에 따라 각각의 다른 속성을 추가
            if (animalInfo is HerbivoreInfo herbivoreInfo)
            {
                writer.WritePropertyName("photosyntesis");
                writer.WriteValue(herbivoreInfo.photosyntesis);
            }
            else if (animalInfo is CarnivoreInfo carnivoreInfo)
            {
                writer.WritePropertyName("damage");
                writer.WriteValue(carnivoreInfo.damage);
            }

            // 쓰기 종료
            writer.WriteEndObject();
        }
        else
        {
            // 만약 Serialize할 정보가 AnimalInfo가 아닐 경우 예외처리를 진행하였다.
            throw new InvalidOperationException("Invalid type for serialization: " + value.GetType().Name);
        }
    }
}